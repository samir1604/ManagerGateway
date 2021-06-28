using DataAccess;
using ManagerGateway.Domain;
using ManagerGateway.Dtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ManagerGateway.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DeviceController : ControllerBase
    {
        private readonly ManagerContext _context;
        public DeviceController(ManagerContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var gateways = _context.Gateways.Include(p => p.Devices).ToList();

            var response = gateways.Select(g => new
            {
                usn = g.Usn,
                name = g.Name,
                address = g.Address,
                devices = g.Devices.Select(d => new
                {
                    id = d.Id,
                    vendor = d.Vendor,
                    status = d.Status.Online
                })
            });
            return Ok(response);
        }

        [HttpGet("{usn}")]
        public IActionResult Get(string usn)
        {
            var gateways = _context.Gateways.Include(p => p.Devices).FirstOrDefault(p => p.Usn == usn);
            if (gateways == null)
            {
                return NotFound(new ResponseHttp
                {
                    success = false,
                    message = "Devices not found"
                }); 
            }

            var response = gateways.Devices.Select(d => new DeviceDto
            {
                Id = d.Id.ToString(),
                Created = d.Created.ToShortDateString(),
                GatewayUsn = d.GatewayUsn,
                Vendor = d.Vendor,
                Status = d.Status.Online
            });
            return Ok(new ResponseHttp
            {
                success = true,
                data = response
            });
        }

        [HttpPost]
        public IActionResult Add(DeviceRequest request)
        {
            try
            {
                var gateway = _context.Gateways.Include("Devices").FirstOrDefault(p => p.Usn == request.GatewayUsn);

                if (gateway == null)
                {
                    return NotFound(new ResponseHttp { 
                    success = false,
                    message = "Gateway not found."
                    });
                }   

                if (!gateway.CanAddDevice())
                {
                    throw new Exception("Your gateway do not accept more devices.");
                }

                Random r = new Random();
                var device = new Device(request.Vendor)
                {
                    Id = r.Next(10, 1000),
                    Created = DateTime.Parse(request.Created),
                    GatewayUsn = gateway.Usn,
                    Status = new Status { Online = request.Status }                              
                };

                _context.Devices.Add(device);
                _context.SaveChanges();

                var response = new ResponseHttp
                {
                    success = true,
                    data = new DeviceDto
                    {
                        Id = device.Id.ToString(),
                        Created = device.Created.ToShortDateString(),
                        GatewayUsn = device.GatewayUsn,
                        Vendor = device.Vendor,
                        Status = device.Status.Online
                    }
                };
                return Created("Add", response);
            }
            catch (Exception e)
            {
                return BadRequest(new ResponseHttp
                {
                    success = false,
                    message = e.Message
                });
            }
        }

        [HttpDelete("{Id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                var device = _context.Devices.FirstOrDefault(p => p.Id == id);
                if (device == null)
                {
                    return NotFound(new ResponseHttp
                    {
                        success = false,
                        message = "Device not Found"
                    });
                }

                _context.Devices.Remove(device);
                _context.SaveChanges();
                return Ok(new ResponseHttp
                {
                    success = true
                });
            } catch (Exception e)
            {
                return BadRequest(new ResponseHttp
                {
                    success = false,
                    message = e.Message
                }); 
            }
            
        }
    }
}
