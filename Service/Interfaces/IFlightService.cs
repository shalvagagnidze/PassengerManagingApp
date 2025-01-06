using Microsoft.AspNetCore.Mvc;
using Service.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Interfaces
{
    public interface IFlightService
    {
        Task<IActionResult> GetAllFlights();
        Task<IActionResult> GetFlight(int id);
        Task<IActionResult> AddFlight(FlightModel model);
        Task<IActionResult> UpdateFlight(FlightModel model);
        Task<IActionResult> DeleteFlight(int id);
    }
}
