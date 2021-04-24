using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using HotelWepApp2.Models;
using Microsoft.AspNetCore.Identity;

namespace HotelWepApp2.Data
{
    public class DbHelper
    {
        public static void SeedData(ApplicationDbContext db, UserManager<IdentityUser> userManager)
        {
            SeedJobs(db, userManager);
            SeedGuest(db);
        }

        private static void SeedJobs(ApplicationDbContext db, UserManager<IdentityUser> userManager)
        {
            const string WaiterEmail = "Waiter@localhost";
            const string WaiterPassword = "Waiter2/";
            if (userManager.FindByNameAsync(WaiterEmail).Result == null)
            {
                var waiter = new IdentityUser
                {
                    UserName = WaiterEmail,
                    Email = WaiterEmail
                };
                IdentityResult result = userManager.CreateAsync(waiter, WaiterPassword).Result;
                if (result.Succeeded)
                {
                    var waiterClaim = new Claim("Waiter", "isWaiter");
                    userManager.AddClaimAsync(waiter, waiterClaim);
                }
            }
        }

        public static void SeedGuest(ApplicationDbContext db)
        {
            var r = db.Rooms.FirstOrDefault();
            if (r == null)
            {
                var room1 = new Room();
                var room2 = new Room();
                db.Rooms.Add(room1);
                db.Rooms.Add(room2);
            }
            var g = db.Guests.FirstOrDefault();
            if (g == null)
            {
                var guest = new Guest
                {
                    Type = "Adult",
                    Room = db.Rooms.Find(1)
                };
                var guest2 = new Guest
                {
                    Type = "Kid",
                    Room = db.Rooms.Find(1)
                };
                var guest3 = new Guest
                {
                    Type = "Adult",
                    Room = db.Rooms.Find(2)
                };
                var r1 = db.Rooms.Find(1);
                r.Guests.Add(guest);
                r.Guests.Add(guest2);
                var r2 = db.Rooms.Find(2);
                r.Guests.Add(guest3);
                
                db.Guests.Add(guest);
                db.Guests.Add(guest2);
                db.Guests.Add(guest3);
            }

            db.SaveChanges();
        }
    }
}
