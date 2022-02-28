using System.ComponentModel.DataAnnotations;

namespace GatewaysSysAdminWebAPI.Models
{
    public class PeripheralDevice 
    { 
        [Key]
        public int Id { get; set; }

        [Required]
        public long UId { get; set; }

        [Required]
        public string? DeviceVendor { get; set; }

        [Required]
        public DateTime DtDeviceCreated{ get; set; }

        [Required]
        public bool Online { get; set; }
         
    }
}
