using DineMasterApi.Models;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace DineMasterApi.Data
{
    public class ApplicationDbContext:DbContext
    {


        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        public DbSet<Role> Roles { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<DiningTable> DiningTables { get; set; }
        public DbSet<Reservation> Reservations { get; set; }
        public DbSet<MenuCategory> MenuCategories { get; set; }
        public DbSet<MenuItem> MenuItems { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<Bill> Bills { get; set; }
        public DbSet<Inventory> Inventory { get; set; }
        public DbSet<Expense> Expenses { get; set; }
        public DbSet<DeliveryAddress> DeliveryAddresses { get; set; }
        public DbSet<DeliveryTracking> DeliveryTrackings { get; set; }
        public DbSet<DeliveryOTP> DeliveryOTPs { get; set; }




        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Order>()
                .HasOne(o => o.Bill)
                .WithOne(b => b.Order)
                .HasForeignKey<Bill>(b => b.OrderId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Order>()
                .HasOne(o => o.DeliveryOTP)
                .WithOne(otp => otp.Order)
                .HasForeignKey<DeliveryOTP>(otp => otp.OrderId)
                .OnDelete(DeleteBehavior.Restrict);

             modelBuilder.Entity<User>()
                .HasMany(u => u.Orders)
                .WithOne(o => o.User)
                .HasForeignKey(o => o.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Role>()
                .HasMany(r => r.Users)
                .WithOne(u => u.Role)
                .HasForeignKey(u => u.RoleId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<DiningTable>()
                .HasMany(t => t.Orders)
                .WithOne(o => o.DiningTable)
                .HasForeignKey(o => o.TableId)
                .OnDelete(DeleteBehavior.SetNull);
        }



    }


    


}
