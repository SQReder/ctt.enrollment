using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Enrollment.Web.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Addresses",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Raw = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Addresses", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Trustees",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    AddressId = table.Column<Guid>(nullable: true),
                    Email = table.Column<string>(nullable: true),
                    FirstName = table.Column<string>(nullable: true),
                    Job = table.Column<string>(nullable: true),
                    JobPosition = table.Column<string>(nullable: true),
                    LastName = table.Column<string>(nullable: true),
                    MiddleName = table.Column<string>(nullable: true),
                    PhoneNumber = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Trustees", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Trustees_Addresses_AddressId",
                        column: x => x.AddressId,
                        principalTable: "Addresses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Enrollees",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    AddressId = table.Column<Guid>(nullable: true),
                    AddressSameAsParent = table.Column<bool>(nullable: false),
                    BirthCertificateId = table.Column<Guid>(nullable: false),
                    FirstName = table.Column<string>(nullable: true),
                    LastName = table.Column<string>(nullable: true),
                    MiddleName = table.Column<string>(nullable: true),
                    ParentId = table.Column<Guid>(nullable: true),
                    RelationshipDegree = table.Column<int>(nullable: false),
                    StudyGrade = table.Column<string>(nullable: true),
                    StudyPlaceTitle = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Enrollees", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Enrollees_Addresses_AddressId",
                        column: x => x.AddressId,
                        principalTable: "Addresses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Enrollees_Trustees_ParentId",
                        column: x => x.ParentId,
                        principalTable: "Trustees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Enrollees_AddressId",
                table: "Enrollees",
                column: "AddressId");

            migrationBuilder.CreateIndex(
                name: "IX_Enrollees_ParentId",
                table: "Enrollees",
                column: "ParentId");

            migrationBuilder.CreateIndex(
                name: "IX_Trustees_AddressId",
                table: "Trustees",
                column: "AddressId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Enrollees");

            migrationBuilder.DropTable(
                name: "Trustees");

            migrationBuilder.DropTable(
                name: "Addresses");
        }
    }
}
