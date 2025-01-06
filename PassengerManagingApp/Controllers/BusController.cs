using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Mvc;
using Service.Interfaces;
using Service.Models;

namespace PassengerManagingApp.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BusController : ControllerBase
    {
        private readonly IBusService _busService;
        public BusController(IBusService busService)
        {
            _busService = busService;
        }

        [HttpGet("get-all-buses")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> GetBus()
        {
            return await _busService.GetAllBuses();
        }

        [HttpGet("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> GetBus(int id)
        {
            return await _busService.GetBus(id);
        }

        [HttpPost("add-bus")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> CreateBus(BusModel bus)
        {
            return await _busService.AddBus(bus);
        }

        [HttpPut("update-bus")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> UpdateBus(BusModel bus)
        {
            return await _busService.UpdateBus(bus);
        }


        [HttpPut("delete-bus")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> DeleteBus(int id)
        {
            return await _busService.DeleteBus(id);
        }

    }
}
