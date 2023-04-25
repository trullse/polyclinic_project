using System;
using System.Collections.Generic;
using System.Linq;
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
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
            Database.EnsureCreated();
        }
    }
}
