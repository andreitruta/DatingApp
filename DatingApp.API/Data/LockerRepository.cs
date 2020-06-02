using System.Collections.Generic;
using System.Threading.Tasks;
using DatingApp.API.Models;
using Microsoft.EntityFrameworkCore;

namespace DatingApp.API.Data
{
    public class LockerRepository : ILockerRepository
    {
        private readonly DataContext _context;
        public LockerRepository(DataContext context)
        {
            _context = context;
        }
        public void Add<T>(T entity) where T : class
        {
            _context.Add(entity);
        }

        public async Task<IEnumerable<Locker>> GetLockers()
        {
            var lockers = await _context.Lockers.ToListAsync();

            return lockers;
        }
    }

}