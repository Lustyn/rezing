using System;
using Service = GSFGetClientVersionInfoSvc;

// we'll probably end up with a misc service or something,
// but this works for now
class GSFGetClientVersionInfoHandler
    : IServiceHandler<Service.GSFRequest, Service.GSFResponse>
{
    public Service.GSFResponse Handle(Service.GSFRequest request)
    {
        Console.WriteLine($"Client connected");
        return new Service.GSFResponse
        {
            clientVersionInfo = "0.0.0"
        };
    }
}
