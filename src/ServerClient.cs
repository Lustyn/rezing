using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Sockets;
using System.Threading;

class ServerClient
{
    private Socket socket;

    private bool running;
    private Thread readThread;
    private Thread writeThread;

    private MemoryStream readStream;
    private BinaryWriter writer;

    private GSFIProtocolCodec codec = new GSFBitProtocolCodec(new ServerMessageFactory());

    private GSFBlockingQueue<GSFMessage> messageQueue = new GSFBlockingQueue<GSFMessage>();

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
        if (codec.CheckHasLength(reader, (int)readStream.Position))
        {
            var writingPosition = readStream.Position;
            readStream.Position = 0;
            // WTF were they smoking, smuggling the "number of length bytes" instead of just relying on
            // the reader to advance the stream position?
            var payloadLength = codec.ReadLength(reader, (int)readStream.Length, out _);
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

            var message = codec.ReadMessage(new BinaryReader(new MemoryStream(payload)));

            Console.WriteLine($"Received message: {message}");

            if (message is GSFRequestMessage request)
            {
                GSFService.GSFResponse response = null;
                switch (request.Body)
                {
                    case GSFGetClientVersionInfoSvc.GSFRequest body:
                        response = new GSFGetClientVersionInfoHandler().Handle(body);
                        break;
                    case GSFValidateNameSvc.GSFRequest body:
                        response = new GSFValidateNameHandler().Handle(body);
                        break;
                }
                if (!(response is null))
                {
                    messageQueue.Enqueue(
                        new ServerResponseMessage(
                            ServiceClass.UserServer,
                            request.Header.msgType,
                            response
                        )
                    );
                }
            }
            else
            {
                Console.WriteLine("Received message is not a request");
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
        codec.WriteMessage(message, writer);
        var payload = outStream.ToArray();

        var packetStream = new MemoryStream(8 + payload.Length + 1);
        var packetWriter = new BinaryWriter(packetStream);
        codec.WriteLength((uint)payload.Length + 1, packetWriter);
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
