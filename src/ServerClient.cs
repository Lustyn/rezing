using System;
using System.IO;
using System.Net.Sockets;
using System.Threading;
using Newtonsoft.Json;

class ServerClient
{
    private Socket socket;

    private bool running;
    private Thread readThread;
    private Thread writeThread;

    private MemoryStream readStream;
    private BinaryWriter writer;

    private GSFIProtocolCodec serverCodec = new GSFBitProtocolCodec(new ServerMessageFactory());
    private GSFIProtocolCodec clientCodec = new GSFBitProtocolCodec(new ClientMessageFactory());

    private GSFBlockingQueue<GSFMessage> messageQueue = new GSFBlockingQueue<GSFMessage>();
    private GSFRequestHandler requestHandler = new GSFRequestHandler();

    public ServerClient(Socket receivedSocket)
    {
        socket = receivedSocket;
        readStream = new MemoryStream();
        writer = new BinaryWriter(readStream);
        Start();
    }

    private void Start()
    {
        running = true;
        readThread = new Thread(HandleReads);
        readThread.Start();
        writeThread = new Thread(HandleWrites);
        writeThread.Start();
    }

    private void HandleReads()
    {
        var buffer = new byte[8192];
        while (running)
        {
            HandleRead(buffer);
        }
    }

    public void HandleRead(byte[] buffer)
    {
        int readBytes;
        try
        {
            readBytes = socket.Receive(buffer);
        }
        catch (SocketException)
        {
            running = false;
            return;
        }

        if (readBytes > 0)
        {
            writer.Write(buffer, 0, readBytes);
        }
        else
        {
            running = false;
            return;
        }

        if (readStream.Position == 0)
        {
            return;
        }

        var reader = new BinaryReader(readStream);
        if (serverCodec.CheckHasLength(reader, (int)readStream.Position))
        {
            var writingPosition = readStream.Position;
            readStream.Position = 0;
            // WTF were they smoking, smuggling the "number of length bytes" instead of just relying on
            // the reader to advance the stream position?
            var payloadLength = serverCodec.ReadLength(reader, (int)readStream.Length, out _);
            if (payloadLength > writingPosition)
            {
                readStream.Position = writingPosition;
                return;
            }
            var payload = reader.ReadBytes(payloadLength);
            var endPosition = readStream.Position;

            // Trim the stream by copying the remaining bytes to the start
            var remainingBytes = writingPosition - endPosition;
            if (remainingBytes > 0)
            {
                var remainingBuffer = reader.ReadBytes((int)remainingBytes);
                readStream.Write(remainingBuffer, 0, remainingBuffer.Length);
            }
            readStream.Position = remainingBytes;

            // Output hex payload to console
            Console.WriteLine($"Received payload: {BitConverter.ToString(payload).Replace("-", " ")}");

            var message = serverCodec.ReadMessage(new BinaryReader(new MemoryStream(payload)));

            if (message is GSFRequestMessage request)
            {
                Console.WriteLine($"Received request: {request.Body} {JsonConvert.SerializeObject(request.Body)}");
                var response = requestHandler.Handle(request.Body);
                if (!(response is null))
                {
                    messageQueue.Enqueue(
                        new ServerResponseMessage(
                            request.Header.requestId,
                            ServiceClass.UserServer,
                            request.Header.msgType,
                            response
                        )
                    );
                }
            }
            else
            {
                Console.WriteLine($"Received message is not a request {message} {JsonConvert.SerializeObject(message)}");
            }
        }
    }

    private void HandleWrites()
    {
        while (running)
        {
            HandleWrite();
        }
    }

    private void HandleWrite()
    {
        var peek = messageQueue.Peek();
        if (peek == null)
        {
            return;
        }
        var message = messageQueue.Dequeue();

        var outStream = new MemoryStream();
        var writer = new BinaryWriter(outStream);
        serverCodec.WriteMessage(message, writer);
        var payload = outStream.ToArray();

        // Validate the payload can be deserialized
        try
        {
            var reader = new BinaryReader(new MemoryStream(payload));
            var deserializedMessage = clientCodec.ReadMessage(reader);
        }
        catch (Exception e)
        {
            Console.WriteLine($"Failed to validate outgoing message: {e}");
            return;
        }

        var packetStream = new MemoryStream(8 + payload.Length + 1);
        var packetWriter = new BinaryWriter(packetStream);
        serverCodec.WriteLength((uint)payload.Length + 1, packetWriter);
        packetWriter.Write(payload);
        packetWriter.Write((byte)0);

        try
        {
            var output = packetStream.ToArray();
            Console.WriteLine($"Sending message: {BitConverter.ToString(output).Replace("-", " ")}");

            socket.Send(output);
        }
        catch (SocketException)
        {
            running = false;
        }
    }

    public void Close()
    {
        running = false;
        messageQueue.NotifyAll();
        socket.Close();
    }
}
