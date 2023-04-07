using DALayer;
using Microsoft.Extensions.Logging;
using Models.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLLayer.Repository
{
    public class DeviceRepository : IDeviceRepository
    {
        private readonly DeviceDbContext deviceDbContext;
        private readonly ILogger<DeviceRepository> logger;

        /*public DeviceRepository()
        {

        }*/

        public DeviceRepository(DeviceDbContext deviceDbContext, ILogger<DeviceRepository> logger)
        {
            this.deviceDbContext = deviceDbContext;
            this.logger = logger;
        }

        public Device? CreateNewDevice(Device device)
        {
            if (deviceDbContext.Devices.Find(device.MACID) == null)
            {
                deviceDbContext.Devices.Add(device);
                deviceDbContext.SaveChanges();
                logger.LogInformation("Successfully New Device Created in Database");
                return device;
            }
            else
            {
                logger.LogError("Device Creation Failed");
                return null;
            }
        }

        public Device? DeleteDevice(string id)
        {
            var devices = deviceDbContext.Devices.FirstOrDefault(x => x.MACID == id);
            if (devices == null)
            {
                logger.LogError("Device Deletion Failed");
                return null;
            }
            deviceDbContext.Devices.Remove(devices);
            deviceDbContext.SaveChanges();
            logger.LogInformation("Successfully Device Deleted from Database");
            return devices;
        }

        public List<Device> GetAllDevice()
        {
            return deviceDbContext.Devices.ToList<Device>();
        }

        public Device? GetDeviceById(string id)
        {
            return deviceDbContext.Devices.FirstOrDefault(x => x.MACID == id);
        }

        public Device? UpdateDevice(string id, Device device)
        {
            var devices=deviceDbContext.Devices.FirstOrDefault(x => x.MACID == id);
            if (devices == null)
            {
                logger.LogError("Device Updation Failed");
                return null;
            }
            devices.type = device.type;
            deviceDbContext.SaveChanges();
            logger.LogInformation("Successfully Device Details Updated in Database");
            return devices;
               
        }
    }
}
