using Microsoft.EntityFrameworkCore; 

namespace GatewaysSysAdminWebAPI.Models
{
    public class GatewaysSysAdminDBContext : DbContext
    {
        public GatewaysSysAdminDBContext(DbContextOptions<GatewaysSysAdminDBContext> options) : base(options)
        {
        }
        public DbSet<Gateway> Gateway { get; set; } = null!;
<<<<<<< HEAD
        public DbSet<PeripheralDevice> PeripheralDevice { get; set; } = null!;        
=======
        public DbSet<PeripheralDevice> PeripheralDevice { get; set; } = null!;

        
>>>>>>> b6d3268b6b330c82a26e57f41a8f61beacd74012
    }
}
