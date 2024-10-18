using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

interface IServiceHandler
{
    GSFService.GSFResponse Handle(GSFService.GSFRequest request);
}
