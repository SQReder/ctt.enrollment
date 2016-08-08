using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Enrollment.Web.Migrations
{
    public partial class AddAlternateID : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AlternateId",
                table: "Trustees",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "AlternateId",
                table: "Enrollees",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AlternateId",
                table: "Trustees");

            migrationBuilder.DropColumn(
                name: "AlternateId",
                table: "Enrollees");
        }
    }
}
