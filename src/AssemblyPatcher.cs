using System;
using System.IO;
using System.Linq;
using Mono.Cecil;
using Mono.Cecil.Cil;

public static class AssemblyPatcher
{
    public static MemoryStream PatchAssembly(Stream assemblyStream)
    {
        var assembly = AssemblyDefinition.ReadAssembly(assemblyStream);
        var module = assembly.MainModule;

        // Patch GSFCommsObject
        var gsfCommsObject = module.GetType("GSFCommsObject");

        // Noop Log and LogWarning
        gsfCommsObject.Methods.Where(m => m.Name == "Log").First().NopVoid();
        gsfCommsObject.Methods.Where(m => m.Name == "LogWarning").First().NopVoid();

        // Passthrough LogErrors
        var logError = gsfCommsObject.Methods.Where(m => m.Name == "LogError").ToList();
        foreach (var method in logError)
        {
            // Remove call to UnityEngine Debug.LogError
            var calls = method.Body.Instructions.Where(
                i => i.OpCode == OpCodes.Call
                && i.Operand is MethodReference
                && (i.Operand as MethodReference).Name == "LogError")
                .ToList();

            foreach (var call in calls)
            {
                var index = method.Body.Instructions.IndexOf(call);
                method.Body.Instructions.RemoveAt(index);
                method.Body.Instructions.Insert(index, Instruction.Create(OpCodes.Pop));
            }

            // Remove call to BIManager.Instance.SendExceptionGoogleAnalytics
            var start = method.Body.Instructions.Where(i => i.OpCode == OpCodes.Call && i.Operand is MethodReference && (i.Operand as MethodReference).Name == "get_Instance").First();
            var end = method.Body.Instructions.Where(i => i.OpCode == OpCodes.Callvirt && i.Operand is MethodReference && (i.Operand as MethodReference).Name == "SendExceptionGoogleAnalytics").First();

            var startIndex = method.Body.Instructions.IndexOf(start);
            var endIndex = method.Body.Instructions.IndexOf(end);

            for (int i = startIndex; i <= endIndex; i++)
            {
                method.Body.Instructions.RemoveAt(startIndex);
            }
        }

        // Write assembly
        var stream = new MemoryStream();
        assembly.Write(stream);
        stream.Position = 0;

        // Write to file for manual inspection
        assembly.Write("libs/Assembly-CSharp-Patched.dll");

        return stream;
    }

    public static void NopVoid(this MethodDefinition method)
    {
        if (method.ReturnType.FullName != "System.Void")
        {
            throw new Exception("Method is not void");
        }
        method.Body.Instructions.Clear();
        method.Body.Instructions.Add(Instruction.Create(OpCodes.Ret));
    }
}
