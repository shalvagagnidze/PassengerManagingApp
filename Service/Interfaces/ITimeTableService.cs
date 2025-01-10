using Service.Models.WebScrappingModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Interfaces
{
    public interface ITimeTableService
    {
        Task<TimeTableResponse> GetTimeTableAsync(string city);
    }
}
