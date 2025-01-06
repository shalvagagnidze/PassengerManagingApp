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
    public class DriverRepository : IDriverRepository
    {
        private readonly DbSet<Driver> _dbSet;

        public DriverRepository(PassengerAppDbContext db)
        {
            var dbSet = db.Set<Driver>();
            _dbSet = dbSet;
        }
        public async Task<IEnumerable<Driver>> GetAllAsync()
        {
            return await _dbSet.Include(x => x.Bus).ToListAsync();
        }

        public async Task<Driver> GetByIdAsync(int id)
        {
            var driver = await _dbSet.Include(x => x.Bus).FirstOrDefaultAsync(x => x.Id == id);

            return driver!;
        }

        public async Task AddAsync(Driver driver)
        {
            await _dbSet.AddAsync(driver);
        }

        public void Update(Driver driver)
        {
            _dbSet.Update(driver);
        }

        public void Delete(Driver driver)
        {
            driver.DeleteEntity();
        }

        public async Task DeleteByIdAsync(int id)
        {
            var driver = await _dbSet.FindAsync(id);

            driver!.DeleteEntity();
        }
    }
}
