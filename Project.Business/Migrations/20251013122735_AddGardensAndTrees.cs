using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Project.Business.Migrations
{
    /// <inheritdoc />
    public partial class AddGardensAndTrees : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "CreateDate",
                table: "Students",
                type: "TEXT",
                nullable: false,
                defaultValue: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "UpdateDate",
                table: "Students",
                type: "TEXT",
                nullable: false,
                defaultValue: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.CreateTable(
                name: "Gardens",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    UserId = table.Column<Guid>(type: "TEXT", nullable: false),
                    CreateDate = table.Column<DateTimeOffset>(type: "TEXT", nullable: false),
                    UpdateDate = table.Column<DateTimeOffset>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Gardens", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Trees",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    GardenId = table.Column<Guid>(type: "TEXT", nullable: false),
                    TreeState = table.Column<int>(type: "INTEGER", nullable: false),
                    Finished = table.Column<bool>(type: "INTEGER", nullable: false),
                    Index = table.Column<int>(type: "INTEGER", nullable: false),
                    CreateDate = table.Column<DateTimeOffset>(type: "TEXT", nullable: false),
                    UpdateDate = table.Column<DateTimeOffset>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Trees", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Trees_Gardens_GardenId",
                        column: x => x.GardenId,
                        principalTable: "Gardens",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Trees_GardenId",
                table: "Trees",
                column: "GardenId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Trees");

            migrationBuilder.DropTable(
                name: "Gardens");

            migrationBuilder.DropColumn(
                name: "CreateDate",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "UpdateDate",
                table: "Students");
        }
    }
}
