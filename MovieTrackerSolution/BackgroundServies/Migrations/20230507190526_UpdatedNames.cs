using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BackgroundServices.Migrations
{
    public partial class UpdatedNames : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ReleaseState",
                table: "Movies",
                newName: "ReleaseDate");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ReleaseDate",
                table: "Movies",
                newName: "ReleaseState");
        }
    }
}
