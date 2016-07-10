using Enrollment.DataAccess;
using Enrollment.Model;
using Enrollment.Web.Model;
using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Enrollment.Web.Database
{
    [UsedImplicitly]
    public class EnrollmentDbContext : DbContext
    {
        public EnrollmentDbContext([NotNull] DbContextOptions<EnrollmentDbContext> options) : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }

        public DbSet<Trustee> Trustees { get; set; }
        public DbSet<Enrollee> Enrollees { get; set; }
        public DbSet<Address> Addresses { get; set; }

        public DBRepository<Enrollee> EnrolleeRepository => new EnrollmentRepository(Enrollees);

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Trustee>()
                .HasKey(x => x.Id);

            modelBuilder.Entity<Enrollee>()
                .HasKey(x => x.Id);

            modelBuilder.Entity<Address>()
                .HasKey(x => x.Id);

            modelBuilder.Entity<Trustee>()
                .HasMany(x => x.Applicants)
                .WithOne(x => x.Parent)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Enrollee>()
                .HasOne(x => x.Address);

            base.OnModelCreating(modelBuilder);
        }
    }
}