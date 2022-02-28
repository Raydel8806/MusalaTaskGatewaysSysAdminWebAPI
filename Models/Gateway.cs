using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;



namespace GatewaysSysAdminWebAPI.Models
{
    public class Gateway
    {   
        [Key]
        public int Id { get; set; }

        [Required]
        public string? SerialNumber  { get; set; } 

        [Required] 
        public string? Name { get; set; }
        
        [Required]
        public String? IpAddress { get; set; }

        [Required]
        public List<PeripheralDevice>? LsPeripheralDevices { get; set; }

        [Required]
        public int? MaxClientNumber { get; set; } 
         
    }
}
