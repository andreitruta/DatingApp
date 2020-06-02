using System;

namespace DatingApp.API.Models
{
    public class Locker
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

        // Boolean value: TRUE of FALSE that indicates if locker is vacant or not
        /*
        * TRUE = locker is vacant, can be occupied by a client
        * FALSE = locker is occupied, can't be occupied by a client 
        */
        public bool LockerVacant { get; set; }

        // Boolean value: TRUE of FALSE that indicates if locker is busy or not
        /*
        * TRUE = locker is busy, proximity lockers must pe invalid for check in
        * FALSE = locker is not busy, proximity lockers are available for check in
        */
        public bool LockerBusy { get; set; }

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