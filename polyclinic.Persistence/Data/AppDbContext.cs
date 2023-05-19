using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using polyclinic.Domain.Entities;

namespace polyclinic.Persistence.Data
{
    public class AppDbContext : DbContext
    {
        // Нужно ли? https://metanit.com/sharp/entityframeworkcore/2.2.php -- да!!
        public DbSet<Client> Clients { get; set; }
        public DbSet<Appointment> Appointments { get; set; }
        public DbSet<Doctor> Doctors { get; set; }
        public DbSet<Shift> Shifts { get; set; }
        public DbSet<Talon> Talons { get; set; }
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Client>().HasKey(c => c.Id);
            modelBuilder.Entity<Doctor>().HasKey(d => d.Id);
            modelBuilder.Entity<Appointment>().HasKey(a => a.Id);
            modelBuilder.Entity<Shift>().HasKey(s => s.Id);
            modelBuilder.Entity<Talon>().HasKey(t => t.Id);

            modelBuilder.Entity<Appointment>().HasOne(a => a.Doctor).WithMany(d => d.Appointments).HasForeignKey(a => a.DoctorId);
            modelBuilder.Entity<Appointment>().HasOne(a => a.Client).WithMany(c => c.Appointments).HasForeignKey(a => a.ClientId);
            //modelBuilder.Entity<Shift>().HasMany(s => s.Talons).WithOne(t => t.ShiftSource).HasForeignKey(t => t.ShiftId);
            //modelBuilder.Entity<Talon>().HasOne(t => t.ShiftSource).WithMany(s => s.Talons).HasForeignKey(t => t.ShiftId);

            modelBuilder.Entity<Client>().Property(c => c.Id).ValueGeneratedOnAdd();
            modelBuilder.Entity<Doctor>().Property(d => d.Id).ValueGeneratedOnAdd();
            modelBuilder.Entity<Appointment>().Property(a => a.Id).ValueGeneratedOnAdd();
            modelBuilder.Entity<Shift>().Property(s => s.Id).ValueGeneratedOnAdd();
            modelBuilder.Entity<Talon>().Property(t => t.Id).ValueGeneratedOnAdd();
        }
    }
}
