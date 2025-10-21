using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Project.Business.Migrations
{
    /// <inheritdoc />
    public partial class UpdateGardenTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Date",
                table: "Gardens");

            migrationBuilder.AddColumn<int>(
                name: "Day",
                table: "Gardens",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Month",
                table: "Gardens",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Year",
                table: "Gardens",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Day",
                table: "Gardens");

            migrationBuilder.DropColumn(
                name: "Month",
                table: "Gardens");

            migrationBuilder.DropColumn(
                name: "Year",
                table: "Gardens");

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "Date",
                table: "Gardens",
                type: "TEXT",
                nullable: false,
                defaultValue: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));
        }
    }
}
