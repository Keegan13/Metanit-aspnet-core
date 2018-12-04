using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.Extensions.DependencyInjection;


namespace AspNetCore_3._3.Models
{
    public class Phone
    {
        public int Id { get; set; }
        public string Model { get; set; }
        public string Company { get; set; }
      
        public Decimal Price { get; set; }
    }
    public class Order
    {
        public int Id { get; set; }
        public string User { get; set; }
        public string Address { get; set; }

        public int PhoneId { get; set; }
        public Phone Phone { get; set; }
        
    }
    public class StoreContext : DbContext
    {
        public DbSet<Phone> Phones { get; set; }
        public DbSet<Order> Orders { get; set; }

        public StoreContext(DbContextOptions<StoreContext> options):base(options)
        {
          
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {


            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Phone>().Property(x => x.Price).HasColumnType("decimal(18,7)");

            //modelBuilder.Entity<Order>().HasOne(x=>x.Phone).WithMany()

        }



    }

    public static class PhoneStoreExtensions
    {
        public static void InitializeDb(this StoreContext context)
        {
            if (!context.Phones.Any())
            {

                Phone p = new Phone { Model = "model1", Company = "comp1", Price = 200M };

                context.Phones.Add(p);

                p = new Phone { Model = "model2", Company = "comp2", Price = 400M };

                context.Phones.Add(p);

                p = new Phone { Model = "model3", Company = "comp3", Price = 600M };

                context.Phones.Add(p);

                context.SaveChanges();
            }
        }

        public static IServiceCollection AddStore(this IServiceCollection app,string ConnecionString)
        {
            app.AddDbContext<StoreContext>(optionsBuilder=> 
                optionsBuilder.UseSqlServer(ConnecionString)
            );

            return app;
        }

    }

}
