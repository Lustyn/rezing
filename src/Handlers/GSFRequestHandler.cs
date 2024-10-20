using System;
using Newtonsoft.Json;

class GSFRequestHandler
    : IServiceHandler<GSFService.GSFRequest, GSFService.GSFResponse>
{
    private readonly GSFGetClientVersionInfoHandler gsfGetClientVersionInfoHandler = new GSFGetClientVersionInfoHandler();
    private readonly GSFValidateNameHandler gsfValidateNameHandler = new GSFValidateNameHandler();
    private readonly GSFLoginHandler gsfLoginHandler = new GSFLoginHandler();
    private readonly GSFGetTiersHandler gsfGetTiersHandler = new GSFGetTiersHandler();
    private readonly GSFGetStatsTypeHandler gsfGetStatsTypeHandler = new GSFGetStatsTypeHandler();
    private readonly GSFGetCmsNotificationsHandler gsfGetCmsNotificationsHandler = new GSFGetCmsNotificationsHandler();
    private readonly GSFGetNotificationByPlayerIdHandler gsfGetNotificationByPlayerIdHandler = new GSFGetNotificationByPlayerIdHandler();

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
            case GSFGetTiersSvc.GSFRequest body:
                return gsfGetTiersHandler.Handle(body);
            case GSFGetStatsTypeSvc.GSFRequest body:
                return gsfGetStatsTypeHandler.Handle(body);
            case GSFGetCmsNotificationsSvc.GSFRequest body:
                return gsfGetCmsNotificationsHandler.Handle(body);
            case GSFGetNotificationByPlayerIdSvc.GSFRequest body:
                return gsfGetNotificationByPlayerIdHandler.Handle(body);
        }

        Console.WriteLine($"Unhandled request: {request} {JsonConvert.SerializeObject(request)}");
        return null;
    }
}
