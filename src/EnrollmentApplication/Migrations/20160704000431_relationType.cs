using EnrollmentApplication.Model;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EnrollmentApplication.Migrations
{
    public partial class relationType : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RelationshipDegree",
                table: "Enrollees");

            migrationBuilder.AddColumn<int>(
                name: "RelationType",
                table: "Enrollees",
                nullable: false,
                defaultValue: RelationTypeEnum.Child);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RelationType",
                table: "Enrollees");

            migrationBuilder.AddColumn<int>(
                name: "RelationshipDegree",
                table: "Enrollees",
                nullable: false,
                defaultValue: 0);
        }
    }
}
