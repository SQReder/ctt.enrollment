using System;
using Enrollment.DataAccess;
using Enrollment.Model;
using JetBrains.Annotations;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Enrollment.EntityFramework
{
    [UsedImplicitly]
    public class ApplicationDbContext : IdentityDbContext<IdentityUser<Guid>, IdentityRole<Guid>, Guid>
    {
        public ApplicationDbContext([NotNull] DbContextOptions<ApplicationDbContext> options) : base(options)
        {
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
            ConfigureAddress(modelBuilder);
            ConfigureEnrollee(modelBuilder);
            ConfigureTrustee(modelBuilder);
            ConfigureUnityGroups(modelBuilder);
            ConfigureUnity(modelBuilder);

            base.OnModelCreating(modelBuilder);
        }

        private static void ConfigureUnity(ModelBuilder modelBuilder)
        {
            var entity = modelBuilder.Entity<Unity>();

            entity
                .HasKey(unity => unity.Id);

            entity
                .HasMany(unity => unity.Admissions)
                .WithOne(admission => admission.Unity)
                .HasForeignKey(admission => admission.UnityId)
                .OnDelete(DeleteBehavior.Restrict);

            entity
                .HasOne(unity => unity.UnityGroup)
                .WithMany(unityGroup => unityGroup.Unities)
                .HasForeignKey(unity => unity.UnityGroupId);
        }

        private static void ConfigureUnityGroups(ModelBuilder modelBuilder)
        {
            var entity = modelBuilder.Entity<UnityGroup>();

            entity
                .HasKey(unityGroup => unityGroup.Id);

            entity
                .HasMany(unityGroup => unityGroup.Unities)
                .WithOne(unity => unity.UnityGroup)
                .OnDelete(DeleteBehavior.Cascade);
        }

        private static void ConfigureAddress(ModelBuilder modelBuilder)
        {
            var entity = modelBuilder.Entity<Address>();

            entity
                .HasKey(x => x.Id);
        }

        private static void ConfigureEnrollee(ModelBuilder modelBuilder)
        {
            var entity = modelBuilder.Entity<Enrollee>();

            entity
                .HasKey(x => x.Id);

            entity
                .HasAlternateKey(x => x.AlternateId);

            entity
                .HasOne(enrollee => enrollee.Address);

            entity
                .HasOne(enrollee => enrollee.Trustee)
                .WithMany(trustee => trustee.Enrollees)
                .HasForeignKey(enrollee => enrollee.TrusteeId);

            entity
                .HasMany(enrollee => enrollee.Admissions)
                .WithOne(admission => admission.Enrollee)
                .HasForeignKey(admission => admission.EnrolleeId)
                .OnDelete(DeleteBehavior.Restrict);
        }

        private static void ConfigureTrustee(ModelBuilder modelBuilder)
        {
            var entity = modelBuilder.Entity<Trustee>();

            entity
                .HasKey(x => x.Id);

            entity
                .HasAlternateKey(x => x.AlternateId);

            entity
                .HasOne(trustee => trustee.Owner)
                .WithOne(applicationUser => applicationUser.Trustee)
                .HasForeignKey<Trustee>(trustee => trustee.OwnerID);

            entity
                .HasMany(trustee => trustee.Enrollees)
                .WithOne(enrollee => enrollee.Trustee)
                .HasForeignKey(enrollee => enrollee.TrusteeId)
                .OnDelete(DeleteBehavior.Cascade);

            entity
                .HasMany(trustee => trustee.Admissions)
                .WithOne(admission => admission.Trustee)
                .HasForeignKey(admission => admission.TrusteeId)
                .OnDelete(DeleteBehavior.Restrict);
        }

        private static void ConfigureAdmission(ModelBuilder modelBuilder)
        {
            var entity = modelBuilder.Entity<Admission>();

            entity
                .HasKey(x => x.Id);

            entity
                .HasAlternateKey(x => x.AlternateId);

            entity
                .HasOne(x => x.Enrollee)
                .WithMany(x => x.Admissions)
                .HasForeignKey(x => x.EnrolleeId)
                .OnDelete(DeleteBehavior.Restrict);

            entity
                .HasOne(x => x.Trustee)
                .WithMany(x => x.Admissions)
                .HasForeignKey(x => x.TrusteeId)
                .OnDelete(DeleteBehavior.Restrict);

            entity
                .HasOne(x => x.Unity)
                .WithMany(x => x.Admissions)
                .HasForeignKey(x => x.UnityId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}