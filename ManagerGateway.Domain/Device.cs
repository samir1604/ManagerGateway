using System;
using System.Collections.Generic;
using System.Text;

namespace ManagerGateway.Domain
{
    public class Device
    { 
        public Device(string vendor)
        {
            if (String.IsNullOrEmpty(vendor))
            {
                throw new ArgumentException("Parameter Vendor is required");
            }

            Vendor = vendor;
        }
        public int Id { get; set; }
        public string GatewayUsn {get; set;}
        public string Vendor { get; private set; }
        public DateTime Created { get; set; }
        public Status Status { get; set; }

        public Gateway Gateway { get; set; }
    }
}
