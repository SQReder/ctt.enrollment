using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Enrollment.Web.Migrations
{
    public partial class Admissions2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Admission_Enrollees_EnrolleeId",
                table: "Admission");

            migrationBuilder.DropForeignKey(
                name: "FK_Admission_Unities_UnityId",
                table: "Admission");

            migrationBuilder.DropUniqueConstraint(
                name: "AK_Admission_AlternateId",
                table: "Admission");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Admission",
                table: "Admission");

            migrationBuilder.AddColumn<Guid>(
                name: "ParentId",
                table: "Admission",
                nullable: true);

            migrationBuilder.AddUniqueConstraint(
                name: "AK_Admissions_AlternateId",
                table: "Admission",
                column: "AlternateId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Admissions",
                table: "Admission",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Admissions_ParentId",
                table: "Admission",
                column: "ParentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Admissions_Enrollees_EnrolleeId",
                table: "Admission",
                column: "EnrolleeId",
                principalTable: "Enrollees",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Admissions_Trustees_ParentId",
                table: "Admission",
                column: "ParentId",
                principalTable: "Trustees",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Admissions_Unities_UnityId",
                table: "Admission",
                column: "UnityId",
                principalTable: "Unities",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.RenameIndex(
                name: "IX_Admission_UnityId",
                table: "Admission",
                newName: "IX_Admissions_UnityId");

            migrationBuilder.RenameIndex(
                name: "IX_Admission_EnrolleeId",
                table: "Admission",
                newName: "IX_Admissions_EnrolleeId");

            migrationBuilder.RenameTable(
                name: "Admission",
                newName: "Admissions");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Admissions_Enrollees_EnrolleeId",
                table: "Admissions");

            migrationBuilder.DropForeignKey(
                name: "FK_Admissions_Trustees_ParentId",
                table: "Admissions");

            migrationBuilder.DropForeignKey(
                name: "FK_Admissions_Unities_UnityId",
                table: "Admissions");

            migrationBuilder.DropUniqueConstraint(
                name: "AK_Admissions_AlternateId",
                table: "Admissions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Admissions",
                table: "Admissions");

            migrationBuilder.DropIndex(
                name: "IX_Admissions_ParentId",
                table: "Admissions");

            migrationBuilder.DropColumn(
                name: "ParentId",
                table: "Admissions");

            migrationBuilder.AddUniqueConstraint(
                name: "AK_Admission_AlternateId",
                table: "Admissions",
                column: "AlternateId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Admission",
                table: "Admissions",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Admission_Enrollees_EnrolleeId",
                table: "Admissions",
                column: "EnrolleeId",
                principalTable: "Enrollees",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Admission_Unities_UnityId",
                table: "Admissions",
                column: "UnityId",
                principalTable: "Unities",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.RenameIndex(
                name: "IX_Admissions_UnityId",
                table: "Admissions",
                newName: "IX_Admission_UnityId");

            migrationBuilder.RenameIndex(
                name: "IX_Admissions_EnrolleeId",
                table: "Admissions",
                newName: "IX_Admission_EnrolleeId");

            migrationBuilder.RenameTable(
                name: "Admissions",
                newName: "Admission");
        }
    }
}
