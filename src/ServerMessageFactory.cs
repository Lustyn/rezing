using System;

public class ServerMessageFactory : GSFBitProtocolCodec.IMessageFactory
{
    public GSFMessage BuildMessage(MessageHeader header)
    {
        Console.WriteLine($"Building message with svcClass: {header.svcClass}, ${header}");
        throw new NotImplementedException(); // TODO: Add mappings for C2S messages
    }
}
