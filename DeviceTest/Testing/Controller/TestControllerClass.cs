using BLLayer.Repository;
using Castle.Core.Logging;
using DevicesAPI.Controllers;
using DeviceTest.MockDatas;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Models.Requests;
using Moq;
using NSubstitute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeviceTest.Testing.Controller
{
    public class TestControllerClass
    {
        [Fact]
        public void GetAllDevices_ShouldReturn200Status()
        {
            //Arrange
            var repository = new Mock<IDeviceRepository>();
            var logger = new Mock<ILogger<DevicesController>>();
            repository.Setup(x=>x.GetAllDevice()).Returns(MockData.GetData());
            var controller = new DevicesController(repository.Object,logger.Object);
            //Act
            var result = controller.GetAllDevices();

            //Assert
            result.GetType().Should().Be(typeof(OkObjectResult));
            (result as OkObjectResult).StatusCode.Should().Be(200);
        }

        [Fact]
        public void GetAllDevices_ShouldReturnNoContent()
        {
            var repository = new Mock<IDeviceRepository>();
            var logger = new Mock<ILogger<DevicesController>>();
            repository.Setup(x => x.GetAllDevice()).Returns(MockData.GetNoData());
            var controller = new DevicesController(repository.Object, logger.Object);

            var result = controller.GetAllDevices();

            result.GetType().Should().Be(typeof(NoContentResult));
        }

        [Fact]
        public void GetDeviceById_ShouldReturn200Status()
        {
            //arrange
            var id = "string1";
            var repository = new Mock<IDeviceRepository>();
            var logger = new Mock<ILogger<DevicesController>>();
            repository.Setup(x => x.GetDeviceById(id)).Returns(MockData.GetDeviceUsingID(id));
            var controller = new DevicesController(repository.Object, logger.Object);

            //act
            var result=controller.GetDeviceById(id);

            //assert
            result.GetType().Should().Be(typeof(OkObjectResult));
            (result as OkObjectResult).StatusCode.Should().Be(200);

        }

        [Fact]
        public void GetDeviceById_ShouldReturnNotFoundObject()
        {
            //arrange
            var id = "string4";
            var repository = new Mock<IDeviceRepository>();
            var logger = new Mock<ILogger<DevicesController>>();
            repository.Setup(x => x.GetDeviceById(id)).Returns(MockData.GetDeviceUsingID(id));
            var controller = new DevicesController(repository.Object, logger.Object);

            //act
            var result = controller.GetDeviceById(id);

            //assert
            result.GetType().Should().Be(typeof(NotFoundObjectResult));
            

        }


        [Fact]
        public void DeleteDevice_ShouldReturn200Status()
        {
            //arrange
            var id = "string1";
            var repository = new Mock<IDeviceRepository>();
            var logger = new Mock<ILogger<DevicesController>>();
            repository.Setup(x => x.DeleteDevice(id)).Returns(MockData.GetDeviceUsingID(id));
            var controller = new DevicesController(repository.Object, logger.Object);

            //act
            var result = controller.DeleteDevice(id);

            //assert
            result.GetType().Should().Be(typeof(OkObjectResult));
            (result as OkObjectResult).StatusCode.Should().Be(200);

        }

        [Fact]
        public void DeleteDevice_ShouldReturnNotFoundObject()
        {
            //arrange
            var id = "string4";
            var repository = new Mock<IDeviceRepository>();
            var logger = new Mock<ILogger<DevicesController>>();
            repository.Setup(x => x.DeleteDevice(id)).Returns(MockData.GetDeviceUsingID(id));
            var controller = new DevicesController(repository.Object, logger.Object);

            //act
            var result = controller.DeleteDevice(id);

            //assert
            result.GetType().Should().Be(typeof(NotFoundObjectResult));


        }

        [Fact]
        public void UpdateDevice_ReturnBadRequestForEmptyRequiredField()
        {
            //arrange
            var id = "string4";
            var device = new UpdateDevice
            {
               
            };
            var repository = new Mock<IDeviceRepository>();
            var logger = new Mock<ILogger<DevicesController>>();
            var controller = new DevicesController(repository.Object, logger.Object);

            //act
            var result = controller.UpdateDevice(id, device);

            //assert 
            result.GetType().Should().Be(typeof(BadRequestObjectResult));


        }

        [Fact]
        public void UpdateDevice_ReturnBadRequestForInvalidTypeField()
        {
            var id = "string12";
            var device = new UpdateDevice
            {
                
                type = Types.None
            };
            var repository = new Mock<IDeviceRepository>();
            var logger = new Mock<ILogger<DevicesController>>();

            var controller = new DevicesController(repository.Object, logger.Object);

            var result = controller.UpdateDevice(id,device);

            result.GetType().Should().Be(typeof(BadRequestObjectResult));

        }

        [Fact]
        public void UpdateDevice_ReturnBadRequestForNotExistingMACID()
        {
            var id = "string12";
            var devices = new UpdateDevice
            {
                type = 0
            };
            var _device = new Device
            {
                MACID=id,
                type=devices.type
            };
            var device = new Device();
            var repository = new Mock<IDeviceRepository>();
            var logger = new Mock<ILogger<DevicesController>>();
            repository.Setup(x => x.UpdateDevice(id,_device)).Returns(MockData.NullData());
            var controller = new DevicesController(repository.Object, logger.Object);

            var result = controller.UpdateDevice(id, devices);

            result.GetType().Should().Be(typeof(BadRequestObjectResult));

        }
        

        [Fact]
        public void CreateNewDevice_ReturnBadRequestForEmptyRequiredField()
        {
            var repository = new Mock<IDeviceRepository>();
            var logger = new Mock<ILogger<DevicesController>>();
            
            var controller = new DevicesController(repository.Object, logger.Object);

            var result = controller.CreateNewDevice(new NewDevice { type = Types.Jasper }) ;


            result.GetType().Should().Be(typeof(BadRequestObjectResult));
        }

        [Fact]
        public void CreateNewDevice_ReturnBadRequestForInvalidTypeField()
        {
            var device = new NewDevice
            {
                MACID = "string12",
                type = Types.None
            };
            var repository = new Mock<IDeviceRepository>();
            var logger = new Mock<ILogger<DevicesController>>();

            var controller = new DevicesController(repository.Object, logger.Object);

            var result = controller.CreateNewDevice(device);

            result.GetType().Should().Be(typeof(BadRequestObjectResult));

        }

        [Fact]
        public void CreateNewDevice_ReturnBadRequestForAlreadyExistingMACID()
        {
            var device = MockData.AddNewDevice();
            var devices = new Device
            {
                MACID = device.MACID,
                type = device.type
            };
            var repository = new Mock<IDeviceRepository>();
            var logger = new Mock<ILogger<DevicesController>>();
            repository.Setup(x => x.CreateNewDevice(devices)).Returns(MockData.NullData());
            var controller = new DevicesController(repository.Object, logger.Object);

            var result = controller.CreateNewDevice(MockData.AddNewDevice());

            result.GetType().Should().Be(typeof(BadRequestObjectResult));

        }
        
        [Fact]
        public void CreateNewDevice_ReturnOkObjectResult()
        {
            var device = MockData.AddNewDevice();
            var devices = new Device
            {
                MACID = device.MACID,
                type = device.type
            };
            var repository = new Mock<IDeviceRepository>();
            var logger = new Mock<ILogger<DevicesController>>();
            var results = MockData.GetDeviceUsingID(device.MACID);
            repository.Setup(x => x.CreateNewDevice(It.IsAny<Device>())).Returns(results);
            var controller = new DevicesController(repository.Object, logger.Object);

            var result = controller.CreateNewDevice(device);

            result.GetType().Should().Be(typeof(OkObjectResult));
        }
        
        
        [Fact]
        public void UpdateDevice_ReturnOkObjectResult()
        {
            string id = "string12";
            var device = new UpdateDevice
            {
                type = Types.Flycatcher
            };
            var devices = new Device
            {
                MACID = id,
                type = device.type
            };
            var repository = new Mock<IDeviceRepository>();
            var logger = new Mock<ILogger<DevicesController>>();

            repository.Setup(x => x.UpdateDevice(It.IsAny<string>(), It.IsAny<Device>())).Returns(devices);
            var controller = new DevicesController(repository.Object,logger.Object);

            var result = controller.UpdateDevice(id, device);

            result.GetType().Should().Be(typeof(OkObjectResult));

        }
    }
}
