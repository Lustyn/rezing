using System.Collections.Generic;
using Service = GSFGetStatsTypeSvc;

public class GSFGetStatsTypeHandler
    : IServiceHandler<Service.GSFRequest, Service.GSFResponse>
{

    public Service.GSFResponse Handle(Service.GSFRequest request)
    {
        return new Service.GSFResponse
        {
            playerStatsTypes = new List<GSFPlayerStatsType>()
        };
    }
}
