using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;

namespace GatewaysSysAdminWebAPI.Models
{
    public class Gateway
    { 
        public int ID{get; set;}

        [Required]
        [Display(Name = "Serial Number")]
        public string? SerialNumber  { get; set; } 

        [Required]
        [Display(Name = "Name")]
        public string? Name { get; set; }
        
        [Required]
        [Display(Name = "IP Address")]
        public String? IpAddress { get; set; }

        [Required]
        public virtual ICollection<PeripheralDevice> LsPeripheralDevices { get; set; } = new PeripheralDevice[] { };

        [Required]
        public int? MaxClientNumber { get; set; } 
         
    }
}