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
    public class GatewayController : ControllerBase
    {
        private readonly ManagerContext _context;
        public GatewayController(ManagerContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var gateways = _context.Gateways.Include(p => p.Devices).ToList();

            var response = gateways.Select(g => new GatewayDto
            {
                Usn = g.Usn,
                Name = g.Name,
                Address = g.Address                
            });
            return Ok(new ResponseHttp
            {
                success = true,
                data = response
            });
        }

        [HttpGet("{id}")]
        public IActionResult Get(string id)
        {
            var gateways = _context.Gateways.Include(p => p.Devices).FirstOrDefault(p => p.Usn == id);

            var response = new GatewayDto
            {
                Usn = gateways.Usn,
                Name = gateways.Name,
                Address = gateways.Address,                
            };
            return Ok(response);
        }

        [HttpPost]
        public IActionResult Add(GatewayRequest request)
        {
            try
            {
                var gateway = new Gateway(
                Guid.NewGuid().ToString(), request.address, request.name);

                _context.Add(gateway);
                _context.SaveChanges();

                var response = new ResponseHttp
                {
                    success = true,
                    data = gateway
                };
                return Ok(response);
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

        [HttpDelete("{id}")]
        public IActionResult Add(string id)
        {
            try
            {
                var gateway = _context.Gateways.FirstOrDefault(p => p.Usn == id);

                if(gateway == null)
                {
                    return NotFound(new ResponseHttp
                    {
                        success = false,
                        message = "Gateway not found"
                    });
                }
                _context.Gateways.Remove(gateway);
                _context.SaveChanges();

                var response = new ResponseHttp
                {
                    success = true
                };
                return Ok(response);
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
    }
}
