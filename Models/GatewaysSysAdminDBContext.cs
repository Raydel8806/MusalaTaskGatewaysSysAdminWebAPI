using Microsoft.EntityFrameworkCore; 

namespace GatewaysSysAdminWebAPI.Models
{
    public class GatewaysSysAdminDBContext : DbContext
    {
        public GatewaysSysAdminDBContext(DbContextOptions<GatewaysSysAdminDBContext> options) : base(options)
        {
        }
        public DbSet<Gateway> Gateway { get; set; } = null!;
        public DbSet<PeripheralDevice> PeripheralDevice { get; set; } = null!;        
    }
}
