using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Enrollment.Model;
using Microsoft.Data.Entity;
using Microsoft.Data.Entity.Metadata;

namespace Enrollment.EntityFramework
{
    public class EnrollmentDbContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }

        public DbSet<Thrustee> Thrustees { get; set; }
        public DbSet<Enrollee> Enrollees { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Thrustee>()
                .HasMany(x => x.Applicants)
                .WithOne(x => x.Parent)
                .OnDelete(DeleteBehavior.Cascade);

            base.OnModelCreating(modelBuilder);
        }
    }
}
