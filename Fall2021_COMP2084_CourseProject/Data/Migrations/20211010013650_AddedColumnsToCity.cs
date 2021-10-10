using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Fall2021_COMP2084_CourseProject.Data.Migrations
{
    public partial class AddedColumnsToCity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "AddedDate",
                table: "Cities",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "Province",
                table: "Cities",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AddedDate",
                table: "Cities");

            migrationBuilder.DropColumn(
                name: "Province",
                table: "Cities");
        }
    }
}
