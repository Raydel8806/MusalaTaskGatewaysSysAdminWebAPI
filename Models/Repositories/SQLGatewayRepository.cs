using Microsoft.EntityFrameworkCore;


namespace GatewaysSysAdminWebAPI.Models
{
    public class SQLGatewayRepository : IGatewayRepository
    {
        private readonly GatewaysSysAdminDBContext _gatewaysSysAdminDBContext;

        public SQLGatewayRepository(GatewaysSysAdminDBContext gatewaysSysAdminDBContext)
        {
            this._gatewaysSysAdminDBContext = gatewaysSysAdminDBContext;
        }

        IEnumerable<Gateway> IGatewayRepository.GetAllGateways()
        {
            return AsyncGetAllGateways().Result;
        }
        async Task<IEnumerable<Gateway>> AsyncGetAllGateways()
        {
            var result = await _gatewaysSysAdminDBContext.Gateway.ToListAsync();
            return result;
        }

        Gateway IGatewayRepository.AddDeviceToGateway(int gatewayId, PeripheralDevice peripheralDevice)
        {
            throw new NotImplementedException();
        }

        Gateway IGatewayRepository.DeleteDeviceFromGateway(int gatewayId, int peripheralDeviceId)
        {
            throw new NotImplementedException();
        }

        Gateway IGatewayRepository.GetGatewayByID(int gatewayId)
        {
            throw new NotImplementedException();
        }

        Gateway IGatewayRepository.AddGateway(Gateway gateway)
        {
            throw new NotImplementedException();
        }

        Gateway? IGatewayRepository.GetGatewayBySerialNumber(string serialNumber)
        {
            throw new NotImplementedException();
        }

        Gateway IGatewayRepository.DeleteGatewayByID(int gatewayId)
        {
            throw new NotImplementedException();
        }
    }
}
