using System;

class GSFRequestHandler
    : IServiceHandler<GSFService.GSFRequest, GSFService.GSFResponse>
{
    private readonly GSFGetClientVersionInfoHandler gsfGetClientVersionInfoHandler = new GSFGetClientVersionInfoHandler();
    private readonly GSFValidateNameHandler gsfValidateNameHandler = new GSFValidateNameHandler();

    public GSFService.GSFResponse Handle(GSFService.GSFRequest request)
    {
        switch (request)
        {
            case GSFGetClientVersionInfoSvc.GSFRequest body:
                return gsfGetClientVersionInfoHandler.Handle(body);
            case GSFValidateNameSvc.GSFRequest body:
                return gsfValidateNameHandler.Handle(body);
        }

        Console.WriteLine($"Unhandled request: {request}");
        return null;
    }
}
