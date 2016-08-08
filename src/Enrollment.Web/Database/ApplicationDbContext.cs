using System;
using Enrollment.DataAccess;
using Enrollment.Model;
using JetBrains.Annotations;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Enrollment.Web.Database
{
    [UsedImplicitly]
    public class ApplicationDbContext : IdentityDbContext<IdentityUser<Guid>, IdentityRole<Guid>, Guid>
    {
        public ApplicationDbContext([NotNull] DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }

        public DbSet<Trustee> Trustees { get; set; }
        public DbSet<Enrollee> Enrollees { get; set; }
        public DbSet<Address> Addresses { get; set; }
        public DbSet<Unity> Unities { get; set; }
        public DbSet<UnityGroup> UnityGroups { get; set; }
        public DbSet<Admission> Admissions { get; set; }

        public DBRepository<Enrollee> EnrolleeRepository => new EnrollmentRepository(Enrollees);

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            /* id */

            modelBuilder.Entity<Trustee>().HasKey(x => x.Id);
            modelBuilder.Entity<Enrollee>().HasKey(x => x.Id);
            modelBuilder.Entity<Address>().HasKey(x => x.Id);

            /* alternate ids */

            modelBuilder.Entity<Trustee>().HasAlternateKey(x => x.AlternateId);
            modelBuilder.Entity<Enrollee>().HasAlternateKey(x => x.AlternateId);
            modelBuilder.Entity<Admission>().HasAlternateKey(x => x.AlternateId);

            /* trustee owner */

            modelBuilder.Entity<Trustee>()
                .HasOne(x => x.Owner)
                .WithOne(x => x.Trustee)
                .HasForeignKey<Trustee>(x => x.OwnerID);

            /* trustee chilren */

            modelBuilder.Entity<Trustee>()
                .HasMany(x => x.Applicants)
                .WithOne(x => x.Parent)
                .OnDelete(DeleteBehavior.Cascade);


            modelBuilder.Entity<Trustee>()
                .HasMany(x => x.Applicants)
                .WithOne(x => x.Parent)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Trustee>()
                .HasMany(x => x.Admissions)
                .WithOne(x => x.Parent)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Enrollee>()
                .HasOne(x => x.Address);

            modelBuilder.Entity<UnityGroup>()
                .HasMany(x => x.Unities)
                .WithOne(x => x.UnityGroup);

            /* admission foreign keys */

            modelBuilder.Entity<Admission>()
                .HasOne(x => x.Parent)
                .WithMany(x => x.Admissions)
                .HasForeignKey(x => x.ParentId);

            modelBuilder.Entity<Admission>()
                .HasOne(x => x.Enrollee)
                .WithMany(x => x.Admissions)
                .HasForeignKey(x => x.EnrolleeId);

            modelBuilder.Entity<Admission>()
                .HasOne(x => x.Unity)
                .WithMany(x => x.Admissions)
                .HasForeignKey(x => x.UnityId);

            base.OnModelCreating(modelBuilder);
        }
    }
}