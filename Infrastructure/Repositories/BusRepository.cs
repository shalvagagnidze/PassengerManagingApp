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
    public class BusRepository : IBusRepository
    {
        private readonly DbSet<Bus> _dbSet;

        public BusRepository(PassengerAppDbContext db)
        {
            var dbSet = db.Set<Bus>();
            _dbSet = dbSet;
        }

        public async Task<IEnumerable<Bus>> GetAllAsync()
        {
            return await _dbSet.ToListAsync();
        }

        public async Task<Bus> GetByIdAsync(int id)
        {
            var bus = await _dbSet.FindAsync(id);

            return bus!;
        }

        public async Task AddAsync(Bus bus)
        {
            await _dbSet.AddAsync(bus);
        }

        public void Update(Bus bus)
        {
            _dbSet.Update(bus);
        }

        public void Delete(Bus bus)
        {
            bus.DeleteEntity();
        }

        public async Task DeleteByIdAsync(int id)
        {
            var bus = await _dbSet.FindAsync(id);

            bus!.DeleteEntity();
        }
    }
}
