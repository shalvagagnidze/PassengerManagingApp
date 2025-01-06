using Domain.Interfaces;
using Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly PassengerAppDbContext _db;
        private readonly BusRepository _busRepository;
        private readonly DriverRepository _driverRepository;
        private readonly FlightRepository _flightRepository;

        public UnitOfWork(PassengerAppDbContext db)
        {
            _db = db;
            _busRepository = new BusRepository(_db);
            _driverRepository = new DriverRepository(_db);
            _flightRepository = new FlightRepository(_db);
        }

        public IBusRepository BusRepository => _busRepository;
        public IDriverRepository DriverRepository => _driverRepository;
        public IFlightRepository FlightRepository => _flightRepository;
        public async Task SaveAsync()
        {
            await _db.SaveChangesAsync();
        }

    }
}
