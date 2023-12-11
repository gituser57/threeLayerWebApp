using DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Reflection.PortableExecutable;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace DAL
{
    public class PizzaDbContext : DbContext
    {
        public PizzaDbContext(DbContextOptions<PizzaDbContext> options) : base(options)
        {
            //Database.EnsureDeleted();
            Database.EnsureCreated();
        }

        public PizzaDbContext()
        {
            //Database.EnsureDeleted();
            Database.EnsureCreated();
        }

        public DbSet<Pizza> Pizzas { get; set; } = null!;

        public DbSet<Manufacturer> Manufacturers { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

            optionsBuilder.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=Pizzaappdb;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            Manufacturer m1 = new Manufacturer { Id = 1, Name = "M1", Country = "UA" };
            Manufacturer m2 = new Manufacturer { Id = 2, Name = "M2", Country = "PL" };

            Pizza pizza1 = new Pizza { Id = 1, Name = "AAAAAAAAAAA", Price = 78, ManufacturerId = m1.Id };
            Pizza pizza2 = new Pizza { Id = 2, Name = "BBBBBBBBBBBBBBB", Price = 33, ManufacturerId = m1.Id };
            Pizza pizza3 = new Pizza { Id = 3, Name = "CCCCCCCCCCCCC", Price = 67, ManufacturerId = m2.Id };
            Pizza pizza4 = new Pizza { Id = 4, Name = "DDDDDDDDDDDDDDD", Price = 88, ManufacturerId = m2.Id };

            

            modelBuilder.Entity<Manufacturer>().HasData(m1,m2);
            modelBuilder.Entity<Pizza>().HasData(pizza1, pizza2, pizza3, pizza4);

            //        modelBuilder.Entity<Post>()
            //.HasOne(e => e.Blog)
            //.WithMany(e => e.Posts)
            //.HasForeignKey(e => e.BlogId)
            //.IsRequired();

        }
    }
}