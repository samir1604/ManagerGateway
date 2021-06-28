using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ManagerGateway.Dtos
{
    public class ResponseHttp
    {
        public bool success { get; set; }
        public string message { get; set; }
        public object data { get; set; }
    }
}
