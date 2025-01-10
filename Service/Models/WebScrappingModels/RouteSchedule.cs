using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Models.WebScrappingModels
{
    public class RouteSchedule
    {
        public string? DepartureTime { get; set; }
        public string? ArrivalTime { get; set; }
        public List<string> Destinations { get; set; } = new();
    }
}
