using Microsoft.AspNetCore.Mvc;
using Service.Interfaces;
using static Service.Services.TimeTableService;

namespace PassengerManagingApp.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TimeTableController : ControllerBase
    {
        private readonly ITimeTableService _timeTableService;

        public TimeTableController(ITimeTableService timeTableService)
        {
            _timeTableService = timeTableService;
        }

        [HttpGet("get-timetable")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> GetTimeTable(string city = "city_1")
        {
            try
            {
                var result = await _timeTableService.GetTimeTableAsync(city);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest($"Error fetching timetable: {ex.Message}");
            }
        }
    }
}
