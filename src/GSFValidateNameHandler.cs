using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class GSFValidateNameHandler : IServiceHandler
{
    // given a username, returns an empty or null string if the username is valid (i.e. not filtered)
    // presumably the string should be filled with the filtered words, but this is never used clientside
    public GSFService.GSFResponse Handle(GSFService.GSFRequest request)
    {
        return new GSFValidateNameSvc.GSFResponse
        {
            filterName = null
        };
    }
}
