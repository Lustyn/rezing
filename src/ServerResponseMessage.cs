using System;
using System.Threading;


/// <summary>
/// For some reason, the original implementation of SerializeMembers on GSFResponseMessage
/// refuses to support serialization.
/// </summary>
public class ServerResponseMessage : GSFMessage
{
    private static int callCount = 0;

    private new MessageHeader header;
    private GSFService.GSFResponse body;
    private Type bodyType;

    public ServerResponseMessage(MessageHeader header, Type bodyType)
    {
        this.header = header;
        this.bodyType = bodyType;
    }

    public ServerResponseMessage(ServiceClass serviceClass, int messageType, GSFService.GSFResponse body)
    {
        this.header = new MessageHeader(Interlocked.Increment(ref callCount));
        header.svcClass = (int)serviceClass;
        header.msgType = messageType;
        this.body = body;
        this.bodyType = body.GetType();
    }

    public override void SerializeMembers(ProtocolType protocol, GSFIProtocolOutput output)
    {
        output.Write(header);
        output.Write(body);
    }

    public override void DeserializeMembers(ProtocolType protocol, GSFIProtocolInput input)
    {
        if (header == null)
        {
            header = (MessageHeader)input.ReadObject(typeof(MessageHeader));
        }
        body = (GSFService.GSFResponse)input.ReadObject(bodyType);
    }
}
