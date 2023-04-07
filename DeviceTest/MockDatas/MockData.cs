using Microsoft.AspNetCore.Identity;
using Models.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeviceTest.MockDatas
{
    public class MockData
    {
        public static List<Device> GetData()
        {
            return new List<Device>
            {
                new Device
                {
                    MACID="string1",
                    type=Types.Jasper
                },
                new Device
                {
                    MACID="string2",
                    type=Types.Jasper
                },
                new Device
                {
                    MACID="string3",
                    type=Types.Flycatcher
                },
                new Device
                {
                    MACID = "string12",
                    type = Types.Jasper
                }
            };
        }

        public static List<Device> GetNoData()
        {
            return new List<Device>();
        }

        public static Device? NullData()
        {
            return null;
        }
        public static Device? GetDeviceUsingID(string id)
        {
            var device = GetData();
            foreach (var _device in device)
            {
                if (_device.MACID == id)
                {
                    return new Device
                    {
                        MACID = _device.MACID,
                        type = _device.type
                    };
                }
            }
            return null;
        }

       

        public static NewDevice AddNewDevice()
        {
            return new NewDevice
            {
                MACID = "string12",
                type = Types.Jasper
            };
        }


       

        
        
    }
}
