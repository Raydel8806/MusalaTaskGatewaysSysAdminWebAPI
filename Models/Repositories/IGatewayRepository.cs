namespace GatewaysSysAdminWebAPI.Models
{
    public interface IGatewayRepository
    {
        IEnumerable<Gateway> GetAllGateways();
        Gateway GetGatewayByID(int gatewayId);

        Gateway AddGateway(Gateway gateway);

        Gateway? GetGatewayBySerialNumber(string serialNumber);

        Gateway DeleteDeviceFromGateway(int gatewayId, int peripheralDeviceId);

        Gateway AddDeviceToGateway(int gatewayId, PeripheralDevice peripheralDevice);

        Gateway DeleteGatewayByID(int gatewayId);
    }
}
