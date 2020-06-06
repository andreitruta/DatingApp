using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using DatingApp.API.Dtos;
using DatingApp.API.Models;
using Microsoft.EntityFrameworkCore;

namespace DatingApp.API.Data
{
    public class AuthRepository : IAuthRepository
    {
        private readonly DataContext _context;
        public AuthRepository(DataContext context)
        {
            _context = context;

        }
        public async Task<User> Login(string username, string password, IEnumerable<Locker> lockers, IMapper mapper)
        {
            var user = await _context.Users.Include(p => p.Photos).FirstOrDefaultAsync(x => x.Username == username);

            if (user == null)
                return null;

            if (!VerifyPasswordHash(password, user.PasswordHash, user.PasswordSalt))
                return null;

            await lockerCheckInAsync(user, lockers, mapper);

            return user;
        }

        private async Task lockerCheckInAsync(User user, IEnumerable<Locker> lockers, IMapper mapper)
        {
            int lockerIdForCheckIn = 0; //default value
            lockerIdForCheckIn = getAvailableLockerId(lockers);

            if (lockerIdForCheckIn != 0)
            {
                if (await lockerUpdateAsync(lockerIdForCheckIn, user))
                {
                    //ok alertify with locker ID
                    Console.WriteLine("Locker for check in: " + lockerIdForCheckIn);
                }
                else
                {
                    //error at locker allocation
                    /*do it manual*/
                    Console.WriteLine("Error at DB!");
                }
            }
            else
            {
                Console.WriteLine("Error no locker available in current configuration!");
            }


            // locker.LockerCheckIn = DateTime.Now;
            // locker.LockerUserId = user.Id;
            // locker.LockerId = lockers.Last().LockerId + 1;
            // locker.LockerId = getAvailableLockerId(lockers);





            // lockerFromRepo.Result.LockerCheckIn = DateTime.Now;
            // await mapper.Map(lockerToUpdate, lockerFromRepo);





            //do not add locker
            //manage ERROR code for lockers



            // await _context.Lockers.AddAsync(locker); //function for adding lockers to the DB 
            // await _context.Update(locker);
            // await _context.SaveChangesAsync();


        }

        private async Task<bool> lockerUpdateAsync(int lockerId, User user)
        {
            var lockerFromRepo = _context.Lockers.FirstOrDefaultAsync(locker => locker.LockerId == lockerId);
            lockerFromRepo.Result.LockerUserId = user.Id;
            lockerFromRepo.Result.LockerVacant = false;
            lockerFromRepo.Result.LockerBusy = true;
            lockerFromRepo.Result.LockerCheckIn = DateTime.Now;
            lockerFromRepo.Result.LockerCheckOut = new DateTime(2000, 01, 1);

            /* greater than 0 = number of changes returns true
            *  == 0 noting saved and returns false */
            return await _context.SaveChangesAsync() > 0;

        }

        static int maxNumberOfLockers = 20;

        static int firstIndexOfTheLocker = 1;
        static int lastIndexOfTheLocker = maxNumberOfLockers;

        static int searchAroundStep = 6;
        int numberOfRows = 2;
        int lockerSpaceing = 4;
        // int lockersPerCycle = 50;

        /* buffer where to store IDs of lockers */
        int[] bufferOfAvailableLockers = new int[maxNumberOfLockers];

        /* buffer where to store IDs of lockers */
        int[] bufferOfBusyLockers = new int[maxNumberOfLockers];

        private void updateVacantAndBusyBuffers(IEnumerable<Locker> lockers)
        {
            /* get the total number of lockers */
            int indexBorder = (int)lockers.LongCount();

            /* update bufferOfLockerIds with IDs of all lockers */
            for (int index = 1; index <= indexBorder; index++)
            {
                if (lockers.ElementAt(index - 1).LockerVacant == true)
                {
                    bufferOfAvailableLockers[index - 1] = 1;
                }
                else
                {
                    bufferOfAvailableLockers[index - 1] = 0;
                }

                if (lockers.ElementAt(index - 1).LockerBusy == true)
                {
                    bufferOfBusyLockers[index - 1] = 1;
                }
                else
                {
                    bufferOfBusyLockers[index - 1] = 0;
                }
            }
        }
        private int getAvailableLockerId(IEnumerable<Locker> lockers)
        {
            /* default value, meaning that there is no locker available */
            int availableLockerId = 0;

            updateVacantAndBusyBuffers(lockers);

            for (int index = firstIndexOfTheLocker; index <= lastIndexOfTheLocker; index++)
            {
                if (bufferOfAvailableLockers[index - 1] == 1)
                {
                    if (searchAroundLocker(index))
                        return index;
                }
            }

            return availableLockerId;
        }

        /* returns TRUE if locker is not busy else return */
        private bool searchAroundLocker(int index)
        {
            int startSearch = 0;
            int stopSearch = 0;
            int correction = 1;

            if (index % 2 == 0)
            {/* parity */
                startSearch = index - searchAroundStep + correction;
                startSearch = (startSearch < firstIndexOfTheLocker) ? firstIndexOfTheLocker : startSearch;

                stopSearch = index + searchAroundStep - 2 * correction;
                stopSearch = (stopSearch > lastIndexOfTheLocker) ? lastIndexOfTheLocker : stopSearch;
            }
            else
            {/* imparity */
                startSearch = index - searchAroundStep + 2 * correction;
                startSearch = (startSearch < firstIndexOfTheLocker) ? firstIndexOfTheLocker : startSearch;

                stopSearch = index + searchAroundStep - correction;
                stopSearch = (stopSearch > lastIndexOfTheLocker) ? lastIndexOfTheLocker : stopSearch;
            }

            for (int searchIndex = startSearch; searchIndex <= stopSearch; searchIndex++)
            {
                if (bufferOfBusyLockers[searchIndex - 1] == 1)
                    return false;
            }

            /* TRUE means that the locker is OK to be checked in => */
            return true;
        }

        private bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512(passwordSalt))
            {
                var computeHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                for (int i = 0; i < computeHash.Length; i++)
                {
                    if (computeHash[i] != passwordHash[i]) return false;
                }
            }
            return true;
        }

        public async Task<User> Register(User user, string password)
        {
            byte[] passwordHash, passwordSalt;
            CreatePasswordHash(password, out passwordHash, out passwordSalt);
            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;

            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();

            return user;
        }

        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }

        public async Task<bool> UserExists(string username)
        {
            if (await _context.Users.AnyAsync(x => x.Username == username))
                return true;

            return false;
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

        public async Task<bool> CheckOut(User user)
        {
            var userId = user.Id;

            var lockerForCheckout = _context.Lockers.FirstOrDefaultAsync(locker => locker.LockerUserId == userId);

            if (lockerForCheckout == null) //not update on NULL
            {
                return true;
            }
            else
            {
                var lockerForHistory = new LockerHistory();

                lockerForHistory.LockerId = lockerForCheckout.Result.LockerId;
                lockerForCheckout.Result.LockerUserId = 0; //set to default
                lockerForCheckout.Result.LockerVacant = true;
                lockerForCheckout.Result.LockerBusy = false;
                lockerForCheckout.Result.LockerCheckOut = DateTime.Now;
                lockerForHistory.LockerCheckIn = lockerForCheckout.Result.LockerCheckIn;
                lockerForHistory.LockerCheckOut = lockerForCheckout.Result.LockerCheckOut;
                lockerForHistory.LockerUserId = userId;
                lockerForHistory.LockerUsername = user.Username;
                lockerForCheckout.Result.LockerCheckIn = new DateTime(2000, 01, 1);

                /* function for adding lockers to the DB  */
                await _context.LockersHistory.AddAsync(lockerForHistory);
                /* greater than 0 = number of changes returns true
                *  == 0 noting saved and returns false */
                return await _context.SaveChangesAsync() > 0;
            }




        }
    }
}