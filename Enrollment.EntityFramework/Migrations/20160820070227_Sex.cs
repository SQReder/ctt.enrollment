using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Enrollment.EntityFramework.Migrations
{
    public partial class Sex : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<byte>(
                name: "Sex",
                table: "Trustees",
                nullable: false,
                defaultValue: global::Enrollment.Model.Sex.Female);

            migrationBuilder.AddColumn<byte>(
                name: "Sex",
                table: "Enrollees",
                nullable: false,
                defaultValue: global::Enrollment.Model.Sex.Female);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Sex",
                table: "Trustees");

            migrationBuilder.DropColumn(
                name: "Sex",
                table: "Enrollees");
        }
    }
}
