using System;
using Enrollment.Web.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Enrollment.Web.Migrations
{
    [DbContext(typeof(EnrollmentDbContext))]
    partial class EnrollmentDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.0.0-rc2-20901")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("EnrollmentApplication.Model.Address", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Raw");

                    b.HasKey("Id");

                    b.ToTable("Addresses");
                });

            modelBuilder.Entity("EnrollmentApplication.Model.Enrollee", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<Guid?>("AddressId");

                    b.Property<bool>("AddressSameAsParent");

                    b.Property<Guid>("BirthCertificateId");

                    b.Property<string>("FirstName");

                    b.Property<string>("LastName");

                    b.Property<string>("MiddleName");

                    b.Property<Guid?>("ParentId");

                    b.Property<int>("RelationType");

                    b.Property<string>("StudyGrade");

                    b.Property<string>("StudyPlaceTitle");

                    b.HasKey("Id");

                    b.HasIndex("AddressId");

                    b.HasIndex("ParentId");

                    b.ToTable("Enrollees");
                });

            modelBuilder.Entity("EnrollmentApplication.Model.Trustee", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<Guid?>("AddressId");

                    b.Property<string>("Email");

                    b.Property<string>("FirstName");

                    b.Property<string>("Job");

                    b.Property<string>("JobPosition");

                    b.Property<string>("LastName");

                    b.Property<string>("MiddleName");

                    b.Property<string>("PhoneNumber");

                    b.HasKey("Id");

                    b.HasIndex("AddressId");

                    b.ToTable("Trustees");
                });

            modelBuilder.Entity("EnrollmentApplication.Model.Enrollee", b =>
                {
                    b.HasOne("EnrollmentApplication.Model.Address")
                        .WithMany()
                        .HasForeignKey("AddressId");

                    b.HasOne("EnrollmentApplication.Model.Trustee")
                        .WithMany()
                        .HasForeignKey("ParentId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("EnrollmentApplication.Model.Trustee", b =>
                {
                    b.HasOne("EnrollmentApplication.Model.Address")
                        .WithMany()
                        .HasForeignKey("AddressId");
                });
        }
    }
}
