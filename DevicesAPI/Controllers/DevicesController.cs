using AutoMapper;
using BLLayer.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Migrations.Operations;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Extensions;
using Models.Requests;
using Models.Responses;

namespace DevicesAPI.Controllers
{
    [ApiController]
    [Route("[Controller]")]
    public class DevicesController : Controller
    {
        private readonly IDeviceRepository deviceRepository;
        private readonly ILogger<DevicesController> logger;

        public DevicesController(IDeviceRepository deviceRepository,ILogger<DevicesController> logger)
        {
            this.deviceRepository = deviceRepository;
            this.logger = logger;
        }


        [HttpGet]
        [Authorize(Roles = "Reader,Writer")]
        public IActionResult GetAllDevices()
        {
            logger.LogInformation("Get All Students Details");
           var devices = deviceRepository.GetAllDevice();
            if (devices.IsNullOrEmpty())
            {
                logger.LogInformation("No Data Found");
                return NoContent();
            }
            
            List<DeviceDTO> device = new List<DeviceDTO>();
            foreach(var _device in devices)
            {
                device.Add(new DeviceDTO
                {
                    MACID = _device.MACID,
                    type=_device.type.ToString()
                });
            }
            logger.LogInformation("Get Operation Completed");
            return Ok(device);
        }

        [HttpGet]
        [Route("{id}")]
        [Authorize(Roles = "Reader,Writer")]
        public IActionResult GetDeviceById(string id)
        {
            var device=deviceRepository.GetDeviceById(id);
            if (device == null)
            {
                logger.LogInformation("Enter the Correct MACID of a device");
                return NotFound("Invalid  MACID");
            }
            var deviceDTO = new DeviceDTO
            {
                MACID = device.MACID,
                type = device.type.ToString()
            };
            logger.LogInformation("One Device Record Found");
            return Ok(deviceDTO);
            
        }

        [HttpPost]
        [Authorize(Roles ="Writer")]
        public IActionResult CreateNewDevice(NewDevice device)
        {
            if (ModelState.IsValid)
            {
                if (device.type == Types.Jasper || device.type == Types.Flycatcher)
                {
                    var devices = new Device
                    {
                        MACID = device.MACID,
                        type = device.type

                    };
                    var _devices = deviceRepository.CreateNewDevice(devices);
                    if (_devices != null)
                    {
                        var deviceDTO = new DeviceDTO
                        {
                            MACID = devices.MACID,
                            type = devices.type.ToString()
                        };
                        logger.LogInformation("Successfully Device Creation Operation Completed");
                        return Ok(deviceDTO);
                    }
                    else
                    {
                        logger.LogError("Entered MACID Already Existed,So New Device Record Not Created");
                        return BadRequest("Enter a Correct MACID");
                    }
                }
                else
                {
                    logger.LogError("Invalid data For machine type");
                    return BadRequest("Enter 1 for Jasper and 2 for Flycatcher");
                }
            }
            else
            {
                logger.LogError("Invalid Data Sent From Client");
                return BadRequest(ModelState);
            }
        }

        [HttpPut]
        [Route("{id}")]
        [Authorize(Roles = "Writer")]
        public IActionResult UpdateDevice(string id, UpdateDevice device)
        {
            if (ModelState.IsValid)
            {
                if (device.type == Types.Jasper || device.type == Types.Flycatcher)
                {
                    var devices = new Device
                    {
                        MACID = id,
                        type = device.type

                    };
                    devices = deviceRepository.UpdateDevice(id, devices);
                    if (devices != null)
                    {
                        var deviceDTO = new DeviceDTO
                        {
                            MACID = devices.MACID,
                            type = devices.type.ToString()
                        };
                        logger.LogInformation("Successfully Device Details Updation Operation Completed");
                        return Ok(deviceDTO);
                    }
                    else
                    {
                        logger.LogError("Entered Invalid MACID ");
                        return BadRequest("Enter a Correct MACID");
                    }
                }
                else
                {
                    logger.LogError("Invalid data For machine type");
                    return BadRequest("Enter 0 for Jasper and 1 for Flycatcher");
                }
            }
            else
            {
                logger.LogError("Invalid Data Sent From Client");
                return BadRequest(ModelState);
            }

        }

        [HttpDelete]
        [Route("{id}")]
        [Authorize(Roles = "Writer")]
        public IActionResult DeleteDevice(string id)
        {
            var device=deviceRepository.DeleteDevice(id);
            if (device == null)
            {
                logger.LogError("Entered Invalid MACID ");
                return NotFound("Enter a correct MACID");
            }
            var deviceDTO = new DeviceDTO
            {
                MACID = device.MACID,
                type = device.type.ToString()
            };
          
            logger.LogInformation("Successfully Device Deletion Operation Completed");
            return Ok(deviceDTO);
        }
    }
}
