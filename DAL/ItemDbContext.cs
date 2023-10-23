using Microsoft.EntityFrameworkCore;
using HouseRenting.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace HouseRenting.DAL 
{
    public class ItemDbContext : IdentityDbContext
    {
        internal readonly object Database;

        public ItemDbContext(DbContextOptions<ItemDbContext> options) : base(options)
        {
           // Database.EnsureCreated();
        }

        public DbSet<Item> Items { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Booking> Bookings { get; set; }
        public DbSet<Image> Images { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseLazyLoadingProxies();
        }
    }
}
