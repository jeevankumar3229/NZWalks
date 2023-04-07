
using Models.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLLayer.Repository
{
    public interface IDeviceRepository
    {
        public List<Device> GetAllDevice();

        public Device? GetDeviceById(string id);

        public Device? CreateNewDevice(Device device);

        public Device? UpdateDevice(string id, Device device);

        public Device? DeleteDevice(string id);
    }
}
