using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using HotelWebApp.Models;

namespace HotelWebApp.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Receptionist> Receptionists { get; set; }
        public DbSet<Chef> Chefs { get; set; }
        public DbSet<Waiter> Waiters { get; set; }
        public DbSet<Guest> Guests { get; set; }
    }
}
