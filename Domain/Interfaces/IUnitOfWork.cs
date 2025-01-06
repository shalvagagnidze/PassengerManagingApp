using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface IUnitOfWork
    {
        IBusRepository BusRepository { get; }
        IDriverRepository DriverRepository { get; }
        IFlightRepository FlightRepository { get; }
        Task SaveAsync();
    }
}
