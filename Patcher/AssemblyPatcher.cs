using System;
using System.Collections.Generic;
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
        gsfCommsObject.Methods.Where(m => m.Name == "Log")
            .First()
            .NopVoid();
        gsfCommsObject.Methods
            .Where(m => m.Name == "LogWarning")
            .First()
            .NopVoid();

        // Passthrough LogErrors
        var logError = gsfCommsObject.Methods
            .Where(m => m.Name == "LogError")
            .ToList();
        foreach (var method in logError)
        {
            // Remove call to UnityEngine Debug.LogError
            var calls = method.Body.Instructions
                .Where(i =>
                    i.OpCode == OpCodes.Call
                    && (i.Operand as MethodReference)?.Name == "LogError"
                )
                .ToList();

            foreach (var call in calls)
            {
                var index = method.Body.Instructions.IndexOf(call);
                method.Body.Instructions.RemoveAt(index);
                method.Body.Instructions.Insert(index, Instruction.Create(OpCodes.Pop));
            }

            // Remove call to BIManager.Instance.SendExceptionGoogleAnalytics
            var start = method.Body.Instructions
                .Where(i =>
                    i.OpCode == OpCodes.Call
                    && (i.Operand as MethodReference)?.Name == "get_Instance"
                )
                .First();
            var end = method.Body.Instructions
                .Where(i =>
                    i.OpCode == OpCodes.Callvirt
                    && (i.Operand as MethodReference)?.Name == "SendExceptionGoogleAnalytics"
                )
                .First();

            var startIndex = method.Body.Instructions.IndexOf(start);
            var endIndex = method.Body.Instructions.IndexOf(end);

            for (int i = startIndex; i <= endIndex; i++)
            {
                method.Body.Instructions.RemoveAt(startIndex);
            }
        }

        var debuggerType = module.GetType("AWMessageFactory");

        var buildMessage = debuggerType.Methods
            .Where(m => m.Name == "BuildMessage")
            .First();

        // Remove call to Debugger.LogError

        var debuggerCall = buildMessage.Body.Instructions
            .Where(i =>
                i.OpCode == OpCodes.Call
                && (i.Operand as MethodReference)?.Name == "LogError"
            )
            .First();

        var debuggerCallIndex = buildMessage.Body.Instructions.IndexOf(debuggerCall);

        buildMessage.Body.Instructions.RemoveAt(debuggerCallIndex);
        buildMessage.Body.Instructions.Insert(debuggerCallIndex, Instruction.Create(OpCodes.Pop));

        // Iterate over all types and find those that implement GSFClientService.GSFRequest
        module.Types.Visit(type => {
            if (type.Implements("GSFIExternalizable"))
            {
                var hasDefaultConstructor = type.Methods
                    .Any(m => m.Name == ".ctor" && m.Parameters.Count == 0);

                if (!hasDefaultConstructor)
                {
                    var defaultConstructor = new MethodDefinition(
                        ".ctor",
                        MethodAttributes.Public | MethodAttributes.HideBySig | MethodAttributes.SpecialName | MethodAttributes.RTSpecialName,
                        module.TypeSystem.Void
                    );
                    defaultConstructor.Body.Instructions.Add(Instruction.Create(OpCodes.Ret));
                    type.Methods.Add(defaultConstructor);
                }
            }
        });

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

    public static void Visit(this IEnumerable<TypeDefinition> types, Action<TypeDefinition> action)
    {
        foreach (var type in types)
        {
            action(type);
            type.NestedTypes.Visit(action);
        }
    }

    public static TypeDefinition LocalResolve(this TypeReference type)
    {
        if (type.Scope.Name == type.Module.Assembly.MainModule.Name) {
            return type.Resolve();
        } else {
            // Skip types from other assemblies
            return null;
        }
    }

    public static void VisitBaseTypes(this TypeDefinition type, Action<TypeDefinition> action)
    {
        var baseType = type.BaseType?.LocalResolve();
        if (baseType != null)
        {
            action(baseType);
            baseType.VisitBaseTypes(action);
        }
    }

    public static bool Implements(this TypeDefinition type, string interfaceName)
    {
        var implements = type.Interfaces.Any(i => i.FullName == interfaceName);
        if (implements)
        {
            return true;
        }
        type.VisitBaseTypes(baseType => {
            if (baseType.Interfaces.Any(i => i.FullName == interfaceName))
            {
                implements = true;
            }
        });
        return implements;
    }
}
