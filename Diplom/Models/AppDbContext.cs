using System.Configuration;
using Microsoft.EntityFrameworkCore;

namespace Diplom.Models
{
    public class AppDbContext : DbContext
    {
        public DbSet<Order> Orders { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Connection> Connections { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            try
            {
                optionsBuilder.UseSqlServer(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);
            }
            catch (Exception)
            {
                throw;
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Таблица Order
            modelBuilder.Entity<Order>(entity =>
            {
                entity.HasKey(e => e.OrderID);

                entity.Property(e => e.OrderName)
                      .IsRequired()
                      .HasMaxLength(150);

                entity.Property(e => e.OrderDescription)
                      .IsRequired()
                      .HasMaxLength(3000);
            });

            // Таблица Employee
            modelBuilder.Entity<Employee>(entity =>
            {
                entity.HasKey(e => e.EmployeeID);

                entity.Property(e => e.FullName)
                      .IsRequired()
                      .HasMaxLength(70);

                entity.Property(e => e.NumberPhone)
                      .IsRequired()
                      .HasMaxLength(15);

                entity.Property(e => e.Email)
                      .IsRequired()
                      .HasMaxLength(100);

                entity.Property(e => e.Adress)
                      .IsRequired()
                      .HasMaxLength(500);

                entity.Property(e => e.PassportSeries)
                      .IsRequired()
                      .HasMaxLength(10);

                entity.Property(e => e.PassportNumber)
                      .IsRequired()
                      .HasMaxLength(10);

                entity.Property(e => e.PassportGivenBy)
                      .IsRequired()
                      .HasMaxLength(300);

                entity.Property(e => e.PassportGivenDateGivenBy)
                      .IsRequired();

                entity.Property(e => e.Registration)
                      .IsRequired()
                      .HasMaxLength(500);

                entity.Property(e => e.INN)
                      .IsRequired()
                      .HasMaxLength(20);

                entity.Property(e => e.SNILS)
                      .IsRequired()
                      .HasMaxLength(20);

                entity.Property(e => e.Post)
                      .HasMaxLength(50);
            });

            // Таблица Connection
            modelBuilder.Entity<Connection>()
                .HasKey(c => new { c.OrderID, c.EmployeeID });

            modelBuilder.Entity<Connection>()
                .HasOne(c => c.Order)
                .WithMany(o => o.Connections)
                .HasForeignKey(c => c.OrderID)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Connection>()
                .HasOne(c => c.Employee)
                .WithMany(e => e.Connections)
                .HasForeignKey(c => c.EmployeeID)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
