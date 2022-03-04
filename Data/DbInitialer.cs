using GatewaysSysAdminWebAPI.Data;
using GatewaysSysAdminWebAPI.Models;

namespace GatewaysSysAdminWebAPI.Data
{
    public class DbInitialer
    {
        public static void Seed(IApplicationBuilder applicationBuilder)
        {
            using (var servicesScope = applicationBuilder.ApplicationServices.CreateScope())
            { 
            } 

        }
    }
}
