using System;

namespace DatingApp.API.Models
{
    public class LockerHistory
    {

        public int Id { get; set; }

        //public ID of the locker 
        /*
        * Locker ID stands for locker number in the locker room
        */
        public int LockerId { get; set; }

        //public user ID of the locker 
        /*
        * Locker user ID stands for the ID that identifies uniquely the user
        * that has been using the locker (helps for history of locker usage)
        */
        public int LockerUserId { get; set; }
        /*
        * Locker Check IN datetime variable is used to see when the client checked in at the reception
        */
        public DateTime LockerCheckIn { get; set; }
        /*
        * Locker Check OUT datetime variable is used to see when the client checked out at the reception,
        * meaning that che client is leaving the gym
        */
        public DateTime LockerCheckOut { get; set; }


    }
}