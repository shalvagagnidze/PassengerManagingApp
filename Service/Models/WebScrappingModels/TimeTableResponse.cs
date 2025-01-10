using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Models.WebScrappingModels
{
    public class TimeTableResponse
    {
        public List<TimeTableEntry> Entries { get; set; } = new();
    }
}
