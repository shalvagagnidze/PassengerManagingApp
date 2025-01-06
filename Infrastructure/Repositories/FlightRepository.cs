using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class FlightRepository : IFlightRepository
    {
        private readonly DbSet<Flight> _dbSet;

        public FlightRepository(PassengerAppDbContext db)
        {
            var dbSet = db.Set<Flight>();
            _dbSet = dbSet;
        }

        public async Task<IEnumerable<Flight>> GetAllAsync()
        {
            return await _dbSet.ToListAsync();
        }

        public async Task<Flight> GetByIdAsync(int id)
        {
            var flight = await _dbSet.FindAsync(id);

            return flight!;
        }

        public async Task AddAsync(Flight flight)
        {
            await _dbSet.AddAsync(flight);
        }

        public void Update(Flight flight)
        {
            _dbSet.Update(flight);
        }

        public void Delete(Flight flight)
        {
            flight.DeleteEntity();
        }

        public async Task DeleteByIdAsync(int id)
        {
            var flight = await _dbSet.FindAsync(id);

            flight!.DeleteEntity();
        }
    }
}
