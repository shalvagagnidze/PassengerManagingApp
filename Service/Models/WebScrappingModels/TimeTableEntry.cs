using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Models.WebScrappingModels
{
    public class TimeTableEntry
    {
        public DateTime Date { get; set; }
        public List<RouteSchedule> Routes { get; set; } = new();
    }
}
