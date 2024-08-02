﻿using Microsoft.EntityFrameworkCore;
using NationalParkAPI_01.Models;

namespace NationalParkAPI_01.Data
{
    public class ApplicationDbContext:DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options):base(options)
        {
        }
        public DbSet<NationalPark> NationalPark { get; set; }
        public DbSet<Trails> Trails { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Booking> Bookings { get; set; }
    }
}
