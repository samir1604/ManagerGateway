using DataAccess;
using ManagerGateway.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ManagerGateway
{
    public class DataGenerator
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new ManagerContext(
                serviceProvider.GetRequiredService<DbContextOptions<ManagerContext>>()))
            {
                if (context.Gateways.Any())
                {
                    return;
                }


                var gateway = new Gateway(Guid.NewGuid().ToString(), "192.168.0.1", "Riverdale");

                var gateway2 = new Gateway(Guid.NewGuid().ToString(), "192.168.0.2", "Mordor");

                var device = new Device("IBM")
                {
                    Id = 1,
                    GatewayUsn = gateway.Usn,                    
                    Created = DateTime.Now,
                    Status = new Status { Online = true }
                };

                var device2 = new Device("DELL")
                {
                    Id = 2,
                    GatewayUsn = gateway2.Usn,
                    
                    Created = DateTime.Now,
                    Status = new Status { Online = false }
                };
                var device3 = new Device("ASUS")
                {
                    Id = 3,
                    GatewayUsn = gateway2.Usn,                    
                    Created = DateTime.Now,
                    Status = new Status { Online = true }
                };

                context.Add(gateway);
                context.Add(gateway2);
                context.Add(device);
                context.Add(device2);
                context.Add(device3);
                context.SaveChanges();
            }
        }
    }
}
