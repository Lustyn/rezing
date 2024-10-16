using System;

/// <summary>
/// Wrapper class around the orignal GSFRequestMessage class.
///
/// There is a bug in the original implementation of DeserializeMembers where an NPE
/// will be thrown if a body is not set. The original code uses <c>body.GetType()</c>
/// instead of <c>bodyType</c> which is set in the constructor.
///
/// Usually, ReadObject won't function on requests however we have patched the assembly
/// to allow this.
/// </summary>
public class ServerRequestMessage : GSFRequestMessage
{
    public ServerRequestMessage(MessageHeader header, Type bodyType) : base(header, bodyType) {}

    public override void DeserializeMembers(ProtocolType protocol, GSFIProtocolInput input)
    {
        if (header == null)
        {
            header = (MessageHeader)input.ReadObject(typeof(MessageHeader));
        }
        body = (GSFService.GSFRequest)input.ReadObject(requestBodyType);
    }
}
