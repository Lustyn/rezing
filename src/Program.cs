using System;
using System.IO;

class Program
{
    private static bool unpatched = false;

    public static void Main(string[] args)
    {
        if (unpatched)
        {
            Console.WriteLine("Running in unpatched mode, game will crash unless a pre-patched Assembly-CSharp.dll is provided");
            var assembly = AppDomain.CurrentDomain.Load(File.ReadAllBytes("bin/Debug/net8.0/Assembly-CSharp-Patched.dll"));
            AppDomain.CurrentDomain.AssemblyResolve += (sender, eventArgs) => {
                if (eventArgs.Name.StartsWith("Assembly-CSharp,"))
                {
                    return assembly;
                }

                return null;
            };
        }
        else
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
        }

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
