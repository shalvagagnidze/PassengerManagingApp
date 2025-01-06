using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Bus : BaseEntity
    {
        public string? Number { get; set; }

        public Driver? Driver { get; set; }
    }
}
