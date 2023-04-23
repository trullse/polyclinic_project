using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace polyclinic.Persistence.Data
{
    public class AppDbContext : DbContext
    {
        // Нужно ли? https://metanit.com/sharp/entityframeworkcore/2.2.php
        // public DbSet<User> Users { get; set; }
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
            Database.EnsureCreated();
        }
    }
}
