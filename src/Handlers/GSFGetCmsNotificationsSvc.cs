using System.Collections.Generic;
using Service = GSFGetCmsNotificationsSvc;

public class GSFGetCmsNotificationsHandler
    : IServiceHandler<Service.GSFRequest, Service.GSFResponse>
{

    public Service.GSFResponse Handle(Service.GSFRequest request)
    {
        return new Service.GSFResponse
        {
            notifications = new List<GSFNotification>()
        };
    }
}
