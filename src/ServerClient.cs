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
        var num = socket.Receive(buffer);
        if (num > 0)
        {
            writer.Write(buffer, 0, num);
        }

        var reader = new BinaryReader(readStream);
        if (codec.CheckHasLength(reader, (int)readStream.Length))
        {
            // TODO: Read length, then read message
            // Also probably need to trim the stream at some point
        }
    }

    public void Close()
    {
        socket.Close();
    }
}
