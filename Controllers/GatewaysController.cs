using Microsoft.AspNetCore.Mvc;
using GatewaysSysAdminWebAPI.Models;
using System.Text;
using System.Text.RegularExpressions;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace GatewaysSysAdminWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GatewaysController : ControllerBase
    {
        private IGatewayRepository gatewayRepository;

        public GatewaysController(IGatewayRepository employeeRepository)
        {
            this.gatewayRepository = employeeRepository;
        }

        // GET: api/<GatewaysController>/1
        [HttpGet]
        public IEnumerable<Gateway> Get()
        {
            return this.gatewayRepository.GetAllGateways();
        }

        // GET api/<GatewaysController>/2
        [HttpGet("{id=int}")]
        public ActionResult<Gateway> Get(int id)
        {
            try
            {
                var result =  this.gatewayRepository.GetGatewayByID(id);
                if (result == null) return NotFound();
                return result;
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError );
            }            
        }

        // PUT api/<GatewaysController>/3
        [HttpPut]
        public ActionResult<Gateway> AddGateway([FromBody] Gateway gateway)
        {
            try
            {
                if (gateway != null)
                {
                    if (gateway.SerialNumber != null)
                    {
                        Regex IPv4Format = new Regex(@"^(([0-9]|[1-9][0-9]|1[0-9]{2}|2[0-4][0-9]|25[0-5]).){3}([0-9]|[1-9][0-9]|1[0-9]{2}|2[0-4][0-9]|25[0-5])$");

                        var testGateway = this.gatewayRepository.GetGatewayBySerialNumber(gateway.SerialNumber);

                        if (testGateway != null)
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
                                    var newGateway = gatewayRepository.AddGateway(gateway);
                                    return newGateway;
                                    // return CreatedAtAction(nameof(AddGateway), new { id = gateway.Id },newGateway);
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
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }

        }
 
        // PATCH api/<GatewaysController>/4
        [HttpPatch]
        public ActionResult<Gateway> Put(int id, [FromBody] PeripheralDevice peripheralDevice)
        {
            try
            { 
                var result = this.gatewayRepository.GetGatewayByID(id);
                if (result != null)
                {
                    if (result.LsPeripheralDevices != null)
                    {
                        if (result.LsPeripheralDevices.Count < result.MaxClientNumber)
                        {
                            result.LsPeripheralDevices.Add(peripheralDevice);
                        }
                    }
                    return result;
                }
                else
                    return NotFound(); ;
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }


        // DELETE api/<GatewaysController>/5
        [HttpDelete("{id}")]
        public ActionResult<Gateway> Delete(int id)
        {
            try
            {
                var nGateway = this.gatewayRepository.GetGatewayByID(id);

                if (nGateway == null)
                {
                    return NotFound($"Gateway with Id = {id} not found.");
                }

                this.gatewayRepository.DeleteGatewayByID(id);

                return nGateway;

            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }
    }
}
