using AutoMapper;
using BusinessLogic.Repository;
using DataAccessLayers.Models;
using DataAccessLayers.Models.DTOs;

using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace DeviceAPI.Controllers
{
    [ApiController]
    [Route("[Controller]")]
    public class DeviceController:Controller
    {
        private readonly IDeviceRepository deviceRepository;
        private readonly IMapper mapper;

        public DeviceController(IDeviceRepository deviceRepository,IMapper mapper)
        {
            this.deviceRepository = deviceRepository;
            this.mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetAllDevices()
        {
            var devices=deviceRepository.GetAllDevice();
            if (devices.IsNullOrEmpty())
            {
                return NoContent();
            }
            var Devices = mapper.Map<List<DeviceDTO>>(devices);
            return Ok(Devices);
        }

        [HttpGet]
        [Route("{id}")]
        public IActionResult GetDeviceById(string id)
        {
            return Ok();
        }

        [HttpPost]
        public IActionResult CreateNewDevice(AddDeviceDTO device)
        {
           // deviceRepository.CreateNewDevice(device);
            return Ok();
        }
        
        [HttpPut]
        [Route("{id}")]
        //https://localhost:8080/Device/abcd123
        public IActionResult UpdateDevice(string id,Device device)
        {
            return Ok();
        }

        [HttpDelete]
        [Route("{id}")]
        public IActionResult DeleteDevice(string id)
        {
            return Ok();
        }
    }
}
