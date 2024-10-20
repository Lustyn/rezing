using System.Collections.Generic;
using Service = GSFGetTiersSvc;

public class GSFGetTiersHandler
    : IServiceHandler<Service.GSFRequest, Service.GSFResponse>
{

    public Service.GSFResponse Handle(Service.GSFRequest request)
    {
        return new Service.GSFResponse
        {
            tiers = new List<GSFTier>()
        };
    }
}
