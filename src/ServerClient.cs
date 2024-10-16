using System;
using System.IO;
using System.Net.Sockets;
using System.Threading;

class ServerClient
{
    private Socket socket;

    private bool running;
    private Thread readThread;

    private MemoryStream readStream;
    private BinaryWriter writer;

    private GSFIProtocolCodec codec = new GSFBitProtocolCodec(new ServerMessageFactory());

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
            Close();
            return;
        }

        if (readBytes <= 0)
        {
            Close();
            return;
        }

        writer.Write(buffer, 0, readBytes);

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
        }
    }

    public void Close()
    {
        running = false;
        socket.Close();
    }
}
