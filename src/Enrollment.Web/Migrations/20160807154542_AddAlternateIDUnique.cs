using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Enrollment.Web.Migrations
{
    public partial class AddAlternateIDUnique : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Admission",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    AlternateId = table.Column<int>(nullable: false),
                    Created = table.Column<DateTime>(nullable: false),
                    EnrolleeId = table.Column<Guid>(nullable: true),
                    UnityId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Admission", x => x.Id);
                    table.UniqueConstraint("AK_Admission_AlternateId", x => x.AlternateId);
                    table.ForeignKey(
                        name: "FK_Admission_Enrollees_EnrolleeId",
                        column: x => x.EnrolleeId,
                        principalTable: "Enrollees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Admission_Unities_UnityId",
                        column: x => x.UnityId,
                        principalTable: "Unities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.AddUniqueConstraint(
                name: "AK_Trustees_AlternateId",
                table: "Trustees",
                column: "AlternateId");

            migrationBuilder.AddUniqueConstraint(
                name: "AK_Enrollees_AlternateId",
                table: "Enrollees",
                column: "AlternateId");

            migrationBuilder.CreateIndex(
                name: "IX_Admission_EnrolleeId",
                table: "Admission",
                column: "EnrolleeId");

            migrationBuilder.CreateIndex(
                name: "IX_Admission_UnityId",
                table: "Admission",
                column: "UnityId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropUniqueConstraint(
                name: "AK_Trustees_AlternateId",
                table: "Trustees");

            migrationBuilder.DropUniqueConstraint(
                name: "AK_Enrollees_AlternateId",
                table: "Enrollees");

            migrationBuilder.DropTable(
                name: "Admission");
        }
    }
}
