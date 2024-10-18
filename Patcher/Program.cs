using System.IO;

class Program
{
    public static void Main(string[] args)
    {
        // Load Assembly-CSharp.dll
        var assemblyStream = File.OpenRead("libs/Assembly-CSharp.dll");

        // Patch Assembly-CSharp.dll
        var patchedStream = AssemblyPatcher.PatchAssembly(assemblyStream);

        // Write patched Assembly-CSharp.dll
        using (var fileStream = File.OpenWrite("obj/Assembly-CSharp-Patched.dll"))
        {
            patchedStream.WriteTo(fileStream);
        }
    }
}
