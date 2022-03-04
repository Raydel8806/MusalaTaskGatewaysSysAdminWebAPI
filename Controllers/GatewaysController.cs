#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GatewaysSysAdminWebAPI.Models;
using System.Text.RegularExpressions;

namespace GatewaysSysAdminWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GatewaysController : ControllerBase
    {
        private IGatewayRepository _gatewayRepository;

        public GatewaysController(IGatewayRepository gatewayRepository)
        {
            _gatewayRepository = gatewayRepository;
        }

        // GET: api/Gateways                            OK
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Gateway>>> GetAllGateway()
        {
            try
            {
                var gateways = await _gatewayRepository.GetAllGateways();
                return gateways;
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }

        }

        // GET: api/Gateways/5                          OK
        [HttpGet("{id}")]
        public async Task<ActionResult<Gateway>> GetGateway(int id)
        {
            try
            { 
                var gateway = await _gatewayRepository.GetGateway(id);

                if (gateway != null)
                {
                    return gateway;
                }
                return NotFound();
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }
                                            
        // PUT: api/Gateways
        // Gateway from body
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754                        OK
        [HttpPut]                                       //OK
        public async Task<ActionResult<Gateway>> PutGateway( Gateway gateway)
        { 
            try
            {
                if (gateway != null)
                {
                    if (gateway.SerialNumber != null)
                    {
                        Regex IPv4Format = new Regex(@"^(([0-9]|[1-9][0-9]|1[0-9]{2}|2[0-4][0-9]|25[0-5]).){3}([0-9]|[1-9][0-9]|1[0-9]{2}|2[0-4][0-9]|25[0-5])$");

                        var isInDBGateway = await _gatewayRepository.GetGatewayBySerialNumber(gateway.SerialNumber);

                        if (isInDBGateway != null)
                        {
                            ModelState.AddModelError("SerialNumber", "Gateway Serial Number already in use.");
                            return BadRequest(ModelState);
                        }
                        else if (gateway.IpAddress != null)
                        {
                            if (!IPv4Format.IsMatch(gateway.IpAddress))
                            {
                                ModelState.AddModelError("IpAddress", "IP Address format error.");

                                return BadRequest(ModelState);
                            }
                            else
                            if (gateway.LsPeripheralDevices != null && gateway.MaxClientNumber != null)
                            {
                                if (gateway.LsPeripheralDevices.Count > gateway.MaxClientNumber)
                                {
                                    ModelState.AddModelError("MaxClientNumber", "Max client number violation.");
                                    return BadRequest(ModelState);
                                }
                                else
                                {
                                    var eGateway = await _gatewayRepository.AddGateway(gateway);

                                    if (eGateway == null)
                                    {
                                        return NotFound(); 
                                    }
                                    else
                                    {
                                        return eGateway;
                                    }
                                }
                            }
                            else
                            {
                                ModelState.AddModelError("NoData", "Empty fields.");
                                return BadRequest(ModelState);
                            }
                        }
                        else
                        {
                            ModelState.AddModelError("NoData", "Empty fields.");
                            return BadRequest(ModelState);
                        }

                    }
                    else
                    {
                        ModelState.AddModelError("NoData", "Empty fields.");
                        return BadRequest(ModelState);

                    }
                }
                else
                {
                    ModelState.AddModelError("NoData", "Empty Object.");
                    return BadRequest(ModelState);
                }
            }
            catch (DbUpdateConcurrencyException DbEx)
            {
                if (await _gatewayRepository.GetGateway(gateway.ID) == null)
                {
                    return NotFound();
                }
                else
                {
                    return NotFound(DbEx.Message);
                }
            }

        }
            
        // UPDATE: api/Gateways                         //OK
        [HttpPatch]
        public ActionResult<Gateway> UpdateGateway(Gateway gateway)
        {
            var eGateway = _gatewayRepository.UpdateGateway(gateway);

            if (eGateway == null)return NotFound();

           return eGateway;
        }

        /*       
       // POST: api/Gateways
       // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
       [HttpPost]
       public async Task<ActionResult<Gateway>> PostGateway(Gateway gateway)
       { 

           _context.Gateway.Add(gateway);
           await _context.SaveChangesAsync();

           return CreatedAtAction("GetGateway", new { id = gateway.ID }, gateway);
       }
        */
        // DELETE: api/Gateways/5                       //OK          
        [HttpDelete("{id}")]
        public async Task<ActionResult<Gateway>> DeleteGateway(int id)
        {
            var eGateway = await _gatewayRepository.GetGateway(id);

            if (eGateway == null)
            {
                return NotFound();
            }
            
            var gateway = await _gatewayRepository.DeleteGateway(id); 
            
            return gateway;

        }

        // ADD Periferical Device: api/Gateways/5                       //OK
        [HttpPost("{id=int}")]
        public async Task<ActionResult<Gateway>> AddPeriphericalDevice(int id,PeripheralDevice gateway)
        {   //Tratamiento de Execciones
            try
            {
                var eGateway = await _gatewayRepository.AddDeviceToGateway(id, gateway);

                if (eGateway == null)
                {
                    return NotFound();
                }

                return  eGateway;
            }
            catch (Exception e)
            {
                return BadRequest(error:e.Message.ToString());
            }
        }

        // UPDATE: api/Gateways/5                      // OK
        //
        [HttpDelete]
        public async Task<ActionResult<Gateway>> DeletePeriphericalDevice(int idGateway, int idPDevice)
        {   //Tratamiento de Execciones

            try
            {
                var eGateway = await _gatewayRepository.DeleteDeviceFromGateway(idGateway, idPDevice);

                if (eGateway == null)
                {
                    return NotFound();
                }

                return eGateway;

            }
            catch (Exception e)
            {
                return BadRequest(error: e.Message.ToString());
            }
        }

    }
}
