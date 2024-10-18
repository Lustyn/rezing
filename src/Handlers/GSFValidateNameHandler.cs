using Service = GSFValidateNameSvc;

public class GSFValidateNameHandler
    : IServiceHandler<Service.GSFRequest, Service.GSFResponse>
{
    // given a username, returns an empty or null string if the username is valid (i.e. not filtered)
    // presumably the string should be filled with the filtered words, but this is never used clientside
    public Service.GSFResponse Handle(Service.GSFRequest request)
    {
        return new Service.GSFResponse
        {
            filterName = null
        };
    }
}
