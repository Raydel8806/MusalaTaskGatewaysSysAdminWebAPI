namespace GatewaysSysAdminWebAPI.Models
{
    public class MockGatewayRepository : IGatewayRepository
    {
        private List<Gateway> _gatewayRepository; 

        public MockGatewayRepository()
        { 
            _gatewayRepository = new List<Gateway>(){
                new Gateway() {SerialNumber = "100010001", Id=1, IpAddress = "1.1.1.1", Name = "Gateway01",
                    LsPeripheralDevices = new List<PeripheralDevice>(){
                        new PeripheralDevice(){ Id = 100,UId = 88061522388, DtDeviceCreated = DateTime.Now, DeviceVendor="ASUS",Online=true},
                        new PeripheralDevice(){ Id = 103,UId = 52238888061, DtDeviceCreated = DateTime.Now, DeviceVendor="BIOSTAR",Online=false},
                        new PeripheralDevice(){ Id = 104,UId = 80615282388, DtDeviceCreated = DateTime.Now, DeviceVendor="DELL",Online=true},
                        new PeripheralDevice(){ Id = 107,UId = 06152288388, DtDeviceCreated = DateTime.Now, DeviceVendor="INTEL",Online=true}
                    }, MaxClientNumber = 10
                },
                new Gateway() {SerialNumber = "100010002", Id=2, IpAddress = "1.1.1.2", Name = "Gateway02",
                    LsPeripheralDevices = new List<PeripheralDevice>(){
                        new PeripheralDevice(){ Id = 120,UId = 88880615223, DtDeviceCreated = DateTime.Now, DeviceVendor="DELL",Online=true},
                        new PeripheralDevice(){ Id = 123,UId = 52888061238, DtDeviceCreated = DateTime.Now, DeviceVendor="INTEL",Online=false},
                        new PeripheralDevice(){ Id = 124,UId = 52828061806, DtDeviceCreated = DateTime.Now, DeviceVendor="DELL",Online=true},
                        new PeripheralDevice(){ Id = 127,UId = 06806288388, DtDeviceCreated = DateTime.Now, DeviceVendor="INTEL",Online=true}
                    }, MaxClientNumber = 10
                }

            };
        }
         
        Gateway IGatewayRepository.AddDeviceToGateway(int gatewayId, PeripheralDevice peripheralDevice)
        {           
            Gateway? gateway = _gatewayRepository.FirstOrDefault(e => e.Id == gatewayId);

            if (gateway != null)
            {
                if (gateway.LsPeripheralDevices == null)
                {
                    gateway.LsPeripheralDevices = new List<PeripheralDevice>() { peripheralDevice };
                }
                else
                {
                    if (gateway.LsPeripheralDevices.Count < gateway.MaxClientNumber)
                    {
                        gateway.LsPeripheralDevices.Add(peripheralDevice);
                    }
                    else
                    {
                        throw new Exception("The maximum number of devices already exists.");
                    }

                }
                return gateway;
            }
            else
            {
                throw new Exception("Unregistred Gateway");
            }
        }

        Gateway IGatewayRepository.AddGateway(Gateway gateway)
        {
            _gatewayRepository.Add(gateway); 
            return gateway;
        }

        Gateway IGatewayRepository.DeleteDeviceFromGateway(int gatewayId, int peripheralDeviceId)
        {
            Gateway? gateway = _gatewayRepository.FirstOrDefault(e => e.Id == gatewayId);

            if (gateway != null)
            {
                if (gateway.LsPeripheralDevices == null)
                {
                    throw new Exception("Unregistred Gateway");
                }
                else{
                    PeripheralDevice? peripheralDevice = gateway.LsPeripheralDevices.FirstOrDefault(e => e.Id == peripheralDeviceId);

                    if (peripheralDevice != null)
                    {
                        gateway.LsPeripheralDevices.Remove(peripheralDevice);
                    }
                    else{
                        throw new Exception("The maximum number of devices already exists.");
                    }

                }
                return gateway;
            }
            else
            {
                throw new Exception("Unregistred Gateway");
            }
        }

        Gateway IGatewayRepository.DeleteGatewayByID(int gatewayId)
        {
            var eGateway = this._gatewayRepository.First(e => e.Id == gatewayId);
 
            if (eGateway != null)
            {
                this._gatewayRepository.Remove(eGateway); 
                return eGateway;
            }
            else
            {
                throw new Exception("Unregistred Gateway");
            }
        }

        IEnumerable<Gateway> IGatewayRepository.GetAllGateways()
        {
            return _gatewayRepository;
        }

        Gateway IGatewayRepository.GetGatewayByID(int gatewayId)
        {
            Gateway? gateway = _gatewayRepository.FirstOrDefault(e => e.Id == gatewayId);
            if (gateway != null)
            {
                return gateway;
            }
            else
            {
                throw new Exception("Unregistred Gateway");
            }
        }

        Gateway? IGatewayRepository.GetGatewayBySerialNumber(string serialNumber)
        {
            Gateway? gateway = _gatewayRepository.FirstOrDefault(e => e.SerialNumber == serialNumber);
            return gateway;
        }
    }
}
