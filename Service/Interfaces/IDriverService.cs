using Microsoft.AspNetCore.Mvc;
using Service.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Interfaces
{
    public interface IDriverService
    {
        Task<IActionResult> GetAllDrivers();
        Task<IActionResult> GetDriver(int id);
        Task<IActionResult> AddDriver(DriverModel model);
        Task<IActionResult> UpdateDriver(DriverModel model);
        Task<IActionResult> DeleteDriver(int id);
    }
}
