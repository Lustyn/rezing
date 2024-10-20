using System;
using Newtonsoft.Json;

class GSFRequestHandler
    : IServiceHandler<GSFService.GSFRequest, GSFService.GSFResponse>
{
    private readonly GSFGetClientVersionInfoHandler gsfGetClientVersionInfoHandler = new GSFGetClientVersionInfoHandler();
    private readonly GSFValidateNameHandler gsfValidateNameHandler = new GSFValidateNameHandler();
    private readonly GSFLoginHandler gsfLoginHandler = new GSFLoginHandler();

    public GSFService.GSFResponse Handle(GSFService.GSFRequest request)
    {
        switch (request)
        {
            case GSFGetClientVersionInfoSvc.GSFRequest body:
                return gsfGetClientVersionInfoHandler.Handle(body);
            case GSFValidateNameSvc.GSFRequest body:
                return gsfValidateNameHandler.Handle(body);
            case GSFLoginSvc.GSFRequest body:
                return gsfLoginHandler.Handle(body);
        }

        Console.WriteLine($"Unhandled request: {request} {JsonConvert.SerializeObject(request)}");
        return null;
    }
}
