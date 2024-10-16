using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Threading;

class GameServer
{
    private TcpListener listener;

    private bool running;
    private Thread connectionThread;

    private List<ServerClient> clients = new List<ServerClient>();

    public GameServer()
    {
        listener = new TcpListener(IPAddress.Loopback, 8080);
    }

    private void HandleConnections()
    {
        running = true;
        while (running)
        {
            HandleConnection();
        }
    }

    private void HandleConnection()
    {
        try
        {
            var client = listener.AcceptSocket();
            Console.WriteLine("Client connected: " + client.RemoteEndPoint);

            var serverClient = new ServerClient(client);
            clients.Add(serverClient);
        }
        catch (SocketException ex)
        {
            if (ex.SocketErrorCode != SocketError.Interrupted)
            {
                Console.WriteLine("Socket error: " + ex.Message);

            }
            running = false;
        }
    }

    public void Start()
    {
        listener.Start();
        connectionThread = new Thread(HandleConnections);
        connectionThread.Start();
    }

    public void Stop()
    {
        listener.Stop();
        foreach (var client in clients)
        {
            client.Close();
        }
    }
}
