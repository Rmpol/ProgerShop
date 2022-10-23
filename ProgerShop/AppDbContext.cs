using Microsoft.EntityFrameworkCore;
using ProgerShop.Models;

namespace ProgerShop
{
    internal class AppDbContext : DbContext
    {

        public DbSet<Item> Items { get; set; }

        public AppDbContext()
        {
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseMySql("server=localhost;port=3306;username=root;password=root;database=wpfshop");
        }

    }
}
