using System;
using System.Threading;


/// <summary>
/// For some reason, the original implementation of SerializeMembers on GSFResponseMessage
/// refuses to support serialization.
/// </summary>
public class ServerResponseMessage : GSFMessage
{
    private static int callCount = 0;

    private readonly MessageHeader header;
    private readonly GSFService.GSFResponse body;

    public ServerResponseMessage(ServiceClass serviceClass, int messageType, GSFService.GSFResponse body)
    {
        this.header = new MessageHeader(Interlocked.Increment(ref callCount));
        header.svcClass = (int)serviceClass;
        header.msgType = messageType;
        this.body = body;
    }

    public override void SerializeMembers(ProtocolType protocol, GSFIProtocolOutput output)
    {
        output.Write(header);
        output.Write(body);
    }
}
