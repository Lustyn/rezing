using System.Collections.Generic;
using Service = GSFGetNotificationByPlayerIdSvc;

public class GSFGetNotificationByPlayerIdHandler
    : IServiceHandler<Service.GSFRequest, Service.GSFResponse>
{

    public Service.GSFResponse Handle(Service.GSFRequest request)
    {
        return new Service.GSFResponse
        {
            playerNotifications = new List<GSFPlayerNotification>()
        };
    }
}
