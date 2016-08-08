using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Enrollment.Web.Migrations
{
    public partial class Admission : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "UnityGroups",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Title = table.Column<string>(maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UnityGroups", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Unities",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Title = table.Column<string>(maxLength: 100, nullable: true),
                    UnityGroupId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Unities", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Unities_UnityGroups_UnityGroupId",
                        column: x => x.UnityGroupId,
                        principalTable: "UnityGroups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Unities_UnityGroupId",
                table: "Unities",
                column: "UnityGroupId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Unities");

            migrationBuilder.DropTable(
                name: "UnityGroups");
        }
    }
}
