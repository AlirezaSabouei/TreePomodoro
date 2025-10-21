using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Project.Business.Migrations
{
    /// <inheritdoc />
    public partial class DeleteFinishedColumnFromTreeTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Finished",
                table: "Trees");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Finished",
                table: "Trees",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);
        }
    }
}
