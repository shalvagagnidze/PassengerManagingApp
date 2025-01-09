using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Service.Interfaces;
using Service.Models;

namespace PassengerManagingApp.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [EnableCors("AllowAllOrigins")]
    public class FlightController : ControllerBase
    {
        private readonly IFlightService _flightService; 
        public FlightController(IFlightService flightService)
        {
            _flightService = flightService;
        }

        [HttpGet("get-all-flights")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> GetFlights()
        {
            return await _flightService.GetAllFlights();
        }

        [HttpGet("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> GetFlight(int id)
        {
            return await _flightService.GetFlight(id);
        }

        [HttpPost("create")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> CreateFlight(FlightModel bus)
        {
            return await _flightService.AddFlight(bus);
        }

        [HttpPut("update")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> UpdateFlight(FlightModel bus)
        {
            return await _flightService.UpdateFlight(bus);
        }


        [HttpPut("delete")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> DeleteFlight(int id)
        {
            return await _flightService.DeleteFlight(id);
        }

    }
}
