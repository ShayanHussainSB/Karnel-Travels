using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace Karnel_Travels.Models
{
    public class database : DbContext
    {
        public DbSet<User> Users { get; set; }

        public DbSet<Travel> Travels { get; set; }

        public DbSet<Accomadtion> Accomadtion { get; set; }

        public DbSet<Tour> Tour { get; set; }

        public DbSet<Tour_Booking> Tour_Bookings { get; set; }

        public DbSet<Travel_Booking> Travels_Booking { get; set; }

        public DbSet<Accomadtion_Booking> Accomadtion_Booking { get; set; }
    }
}