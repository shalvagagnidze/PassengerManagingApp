﻿using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Service.Interfaces;
using Service.Models;

namespace PassengerManagingApp.Controllers
{

    [ApiController]
    [Route("[controller]")]
    [EnableCors("AllowAllOrigins")]
    public class DriverController : ControllerBase
    {
        private readonly IDriverService _driverService;
        public DriverController(IDriverService driverService)
        {
            _driverService = driverService;
        }

        [HttpGet("get-all-drivers")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> GetDriver()
        {
            return await _driverService.GetAllDrivers();
        }

        [HttpGet("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> GetDriver(int id)
        {
            return await _driverService.GetDriver(id);
        }

        [HttpPost("create")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> CreateDriver(DriverModel driver)
        {
            return await _driverService.AddDriver(driver);
        }

        [HttpPut("update")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> UpdateDriver(DriverModel driver)
        {
            return await _driverService.UpdateDriver(driver);
        }


        [HttpPut("delete/{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> DeleteDriver(int id)
        {
            return await _driverService.DeleteDriver(id);
        }

    }
}
