using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using HotelWepApp2.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace HotelWepApp2.Data
{
    public class DbHelper
    {
        public static void SeedData(ApplicationDbContext db, UserManager<IdentityUser> userManager)
        {
            SeedJobs(db, userManager);
            SeedGuest(db);
            SeedBuffet(db);
        }

        private static void SeedJobs(ApplicationDbContext db, UserManager<IdentityUser> userManager)
        {
            const string WaiterEmail = "Waiter@localhost";
            const string WaiterPassword = "Waiter2/";
            const bool EmailConfirmed = true;
            if (userManager.FindByNameAsync(WaiterEmail).Result == null)
            {
                var waiter = new IdentityUser
                {
                    UserName = WaiterEmail,
                    Email = WaiterEmail,
                    EmailConfirmed = EmailConfirmed
                };
                IdentityResult result = userManager.CreateAsync(waiter, WaiterPassword).Result;
                if (result.Succeeded)
                {
                    var waiterClaim = new Claim("Waiter", "isWaiter");
                    userManager.AddClaimAsync(waiter, waiterClaim).Wait();
                }
            }

            const string ChefEmail = "Chef@localhost";
            const string ChefPassword = "Chef2/";
            if (userManager.FindByNameAsync(ChefEmail).Result == null)
            {
                var chef = new IdentityUser
                {
                    UserName = ChefEmail,
                    Email = ChefEmail,
                    EmailConfirmed = EmailConfirmed
                };
                IdentityResult result = userManager.CreateAsync(chef, ChefPassword).Result;
                if (result.Succeeded)
                {
                    var chefClaim = new Claim("Chef", "isChef");
                    userManager.AddClaimAsync(chef, chefClaim).Wait();
                }
            }


            const string ReceptionistEmail = "Receptionist@localhost";
            const string ReceptionistPassword = "Receptionist2/";
            if (userManager.FindByNameAsync(ReceptionistEmail).Result == null)
            {
                var receptionist = new IdentityUser
                {
                    UserName = ReceptionistEmail,
                    Email = ReceptionistEmail,
                    EmailConfirmed = EmailConfirmed
                };
                IdentityResult result = userManager.CreateAsync(receptionist, ReceptionistPassword).Result;
                if (result.Succeeded)
                {
                    var receptionistClaim = new Claim("Receptionist", "IsReceptionist");
                    userManager.AddClaimAsync(receptionist, receptionistClaim).Wait();
                }
            }
        }

        public static void SeedGuest(ApplicationDbContext db)
        {
            var g = db.Guests.FirstOrDefault();
            if (g == null)
            {
                var room1 = new Room();
                var room2 = new Room();
                db.Rooms.Add(room1);
                db.Rooms.Add(room2);

                var guest = new Guest
                {
                    Type = "Adult",
                    Room = room1
                };
                var guest2 = new Guest
                {
                    Type = "Kid",
                    Room = room1
                };
                var guest3 = new Guest
                {
                    Type = "Adult",
                    Room = room2
                };
                db.Guests.Add(guest);
                db.Guests.Add(guest2);
                db.Guests.Add(guest3);

                db.SaveChanges();
            }
        }

        public static void SeedBuffet(ApplicationDbContext db)
        {
            var b = db.Buffets.FirstOrDefault();
            var guests = db.Guests.ToList();
            if (b == null)
            {
                
                List<Guest> tmp = new List<Guest>();
                foreach (var guest in guests)
                {
                    if (guest.GuestId == 2)
                    {
                        tmp.Add(guest);
                    }

                    if (guest.GuestId == 3)
                    {
                        tmp.Add(guest);
                    }
                }

                var buffet = new Buffet()
                {
                    Guest = tmp
                };
                db.Buffets.Add(buffet);
                db.SaveChanges();
            }

            
        }
    }
}
