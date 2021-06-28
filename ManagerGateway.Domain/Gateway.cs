using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace ManagerGateway.Domain
{
    public class Gateway
    {
        const int MAX_DEVICES = 10;
        public Gateway(string usn, string address, string name)
        {
            if (!Regex.IsMatch(address, "^((25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\\.){3}(25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)$"))
            {
                throw new ArgumentException("Parameter address is not a valid ip address.");
            }

            if (String.IsNullOrEmpty(usn))
            {
                throw new ArgumentException("Parameter usn is required");
            }

            if (String.IsNullOrEmpty(name))
            {
                throw new ArgumentException("Parameter name is required");
            }

            Usn = usn;
            Address = address;
            Name = name;

            Devices = new HashSet<Device>();
        }

        public string Usn { get; private set; }
        public string Name { get; private set; }
        public string Address { get; private set; }

        public virtual ICollection<Device> Devices { get; set; }    
        

        public bool CanAddDevice()
        {
            return Devices.Count() < MAX_DEVICES;
        }
    }
}
