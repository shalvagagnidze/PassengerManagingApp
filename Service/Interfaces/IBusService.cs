using Microsoft.AspNetCore.Mvc;
using Service.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Interfaces
{
    public interface IBusService
    {
        Task<IActionResult> GetAllBuses();
        Task<IActionResult> GetBus(int id);
        Task<IActionResult> AddBus(BusModel model);
        Task<IActionResult> UpdateBus(BusModel model);
        Task<IActionResult> DeleteBus(int id);
    }
}
