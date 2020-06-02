using System.Collections.Generic;
using System.Threading.Tasks;
using DatingApp.API.Models;

namespace DatingApp.API.Data
{
    public interface ILockerRepository
    {
        void Add<T>(T entity) where T : class;
        Task<IEnumerable<Locker>> GetLockers();
    }
}
