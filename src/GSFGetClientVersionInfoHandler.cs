using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

// we'll probably end up with a misc service or something,
// but this works for now
class GSFGetClientVersionInfoHandler : IServiceHandler
{
    public GSFService.GSFResponse Handle(GSFService.GSFRequest request)
    {
        Console.WriteLine($"Client connected");
        return new GSFGetClientVersionInfoSvc.GSFResponse
        {
            clientVersionInfo = "0.0.0"
        };
    }
}
