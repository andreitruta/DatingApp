using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using DatingApp.API.Models;

namespace DatingApp.API.Data
{
    public interface IAuthRepository
    {
        Task<User> Register(User user, string password);
        Task<User> Login(string username, string password, IEnumerable<Locker> lockers, IMapper mapper);
        Task<bool> UserExists(string username);
        void Add<T>(T entity) where T : class;
        Task<IEnumerable<Locker>> GetLockers();
        Task<bool> CheckOut(User user);
    }
}