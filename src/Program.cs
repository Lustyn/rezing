using System;
using System.IO;

class Program
{
    public static void Main(string[] args)
    {
        // Load Assembly-CSharp.dll
        var assemblyStream = File.OpenRead("libs/Assembly-CSharp.dll");

        // Patch Assembly-CSharp.dll
        var patchedStream = AssemblyPatcher.PatchAssembly(assemblyStream);
        var patchedAssembly = AppDomain.CurrentDomain.Load(patchedStream.ToArray());
        Console.WriteLine("Assembly-CSharp.dll patched");

        AppDomain.CurrentDomain.AssemblyResolve += (sender, eventArgs) => {
            if (eventArgs.Name.StartsWith("Assembly-CSharp,"))
            {
                return patchedAssembly;
            }

            return null;
        };

        RealMain(args);
    }

    static GameServer server;

    static void RealMain(string[] args)
    {
        server = new GameServer();

        Console.CancelKeyPress += (sender, eventArgs) => {
            eventArgs.Cancel = true;
            Shutdown();
        };

        server.Start();
        Console.WriteLine("Server started");
    }

    static void Shutdown()
    {
        server.Stop();
        Console.WriteLine("Server stopped");
    }
}
