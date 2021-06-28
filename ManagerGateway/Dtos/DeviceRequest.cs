using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ManagerGateway.Dtos
{
    public class DeviceRequest
    {
        public string GatewayUsn { get; set; }
        public string Vendor { get; set; }
        public string Created { get; set; }
        public bool Status { get; set; }
    }
}
