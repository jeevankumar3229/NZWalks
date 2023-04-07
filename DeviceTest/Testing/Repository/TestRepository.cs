using BLLayer.Repository;
using DALayer;
using DevicesAPI.Controllers;
using DeviceTest.MockDatas;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Models.Requests;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeviceTest.Testing.Repository
{

    public class TestRepository : IDisposable
    {
        private readonly DeviceDbContext deviceDbContext;

        public TestRepository()
        {
            var option = new DbContextOptionsBuilder<DeviceDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            deviceDbContext = new DeviceDbContext(option);

            deviceDbContext.Database.EnsureCreated();

        }

        [Fact]
        public void GetAllDevice_ReturnDeviceList()
        {
            //arrange
            deviceDbContext.Devices.AddRange(MockData.GetData());
            deviceDbContext.SaveChanges();
            var logger = new Mock<ILogger<DeviceRepository>>();
            var sut = new DeviceRepository(deviceDbContext, logger.Object);

            //Act
            var result = sut.GetAllDevice();

            //assert
            result.Should().HaveCount(MockData.GetData().Count);

        }

        [Fact]
        public void GetAllDevice_ReturnEmptyList()
        {
            //arrange
            var logger = new Mock<ILogger<DeviceRepository>>();
            var sut = new DeviceRepository(deviceDbContext, logger.Object);

            //Act
            var result = sut.GetAllDevice();

            //assert
            result.Should().HaveCount(0);

        }

        [Fact]
        public void GetDeviceById_ReturnDevice()
        {
            var id = "string1";
            //Arrange
            deviceDbContext.Devices.AddRange(MockData.GetData());
            deviceDbContext.SaveChanges();
            var logger = new Mock<ILogger<DeviceRepository>>();
            var sut = new DeviceRepository(deviceDbContext, logger.Object);

            //Act
            var result = sut.GetDeviceById(id);

            //Assert
            result.Should().Match<Device>(x => x.MACID == id);

        }

        [Fact]
        public void GetDeviceById_Null()
        {
            var id = "string4";
            //Arrange
            deviceDbContext.Devices.AddRange(MockData.GetData());
            deviceDbContext.SaveChanges();
            var logger = new Mock<ILogger<DeviceRepository>>();
            var sut = new DeviceRepository(deviceDbContext, logger.Object);

            //Act
            var result = sut.GetDeviceById(id);

            //Assert
            result.Should().BeNull();

        }

        [Fact]
        public void DeleteDevice_ReturnDevice()
        {
            var id = "string1";
            //Arrange
            deviceDbContext.Devices.AddRange(MockData.GetData());
            deviceDbContext.SaveChanges();
            var logger = new Mock<ILogger<DeviceRepository>>();
            var sut = new DeviceRepository(deviceDbContext, logger.Object);

            //Act
            var result = sut.DeleteDevice(id);

            //Assert
            result.Should().Match<Device>(x => x.MACID == id);


        }

        [Fact]
        public void DeleteDevice_Null()
        {
            var id = "string4";
            //Arrange
            deviceDbContext.Devices.AddRange(MockData.GetData());
            deviceDbContext.SaveChanges();
            var logger = new Mock<ILogger<DeviceRepository>>();
            var sut = new DeviceRepository(deviceDbContext, logger.Object);

            //Act
            var result = sut.DeleteDevice(id);

            //Assert
            result.Should().BeNull();

        }

        [Fact]
        public void CreateNewDevice_ReturnsDeviceObject()
        {
            var device = new Device
            {
                MACID = "string16",
                type = Types.Jasper
            };
            deviceDbContext.Devices.AddRange(MockData.GetData());
            deviceDbContext.SaveChanges();
            var logger = new Mock<ILogger<DeviceRepository>>();
            var sut = new DeviceRepository(deviceDbContext, logger.Object);

            var result = sut.CreateNewDevice(device);

            result.GetType().Should().Be(typeof(Device));
        }

        [Fact]
        public void CreateNewDevice_ReturnsNull()
        {
            var device = new Device
            {
                MACID = "string1",
                type = Types.Jasper
            };
            deviceDbContext.Devices.AddRange(MockData.GetData());
            deviceDbContext.SaveChanges();
            var logger = new Mock<ILogger<DeviceRepository>>();
            var sut = new DeviceRepository(deviceDbContext, logger.Object);

            var result = sut.CreateNewDevice(device);

            result.Should().BeNull();
        }

        [Fact]
        public void UpdateDevice_ReturnsDeviceObject()
        {
            var id = "string1";
            var device = new Device
            {
                MACID = id,
                type = Types.Flycatcher
            };
            deviceDbContext.Devices.AddRange(MockData.GetData());
            deviceDbContext.SaveChanges();
            var logger = new Mock<ILogger<DeviceRepository>>();
            var sut = new DeviceRepository(deviceDbContext, logger.Object);

            var result = sut.UpdateDevice(id, device);

            result.GetType().Should().Be(typeof(Device));
        }

        [Fact]
        public void UpdateDevice_ReturnsNull()
        {
            var id = "string16";
            var device = new Device
            {
                MACID = id,
                type = Types.Flycatcher
            };
            deviceDbContext.Devices.AddRange(MockData.GetData());
            deviceDbContext.SaveChanges();
            var logger = new Mock<ILogger<DeviceRepository>>();
            var sut = new DeviceRepository(deviceDbContext, logger.Object);

            var result = sut.UpdateDevice(id, device);

            result.Should().BeNull();
        }
        public void Dispose()
        {
            deviceDbContext.Database.EnsureDeleted();
            deviceDbContext.Dispose();
        }
    }
}
