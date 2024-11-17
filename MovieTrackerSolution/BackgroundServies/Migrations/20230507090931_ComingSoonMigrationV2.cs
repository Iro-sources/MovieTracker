using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BackgroundServices.Migrations
{
    public partial class ComingSoonMigrationV2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Directors",
                table: "Movies",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Stars",
                table: "Movies",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "People",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    DirectorId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    StarId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_People", x => x.Id);
                    table.ForeignKey(
                        name: "FK_People_Movies_DirectorId",
                        column: x => x.DirectorId,
                        principalTable: "Movies",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_People_Movies_StarId",
                        column: x => x.StarId,
                        principalTable: "Movies",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_People_DirectorId",
                table: "People",
                column: "DirectorId");

            migrationBuilder.CreateIndex(
                name: "IX_People_StarId",
                table: "People",
                column: "StarId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "People");

            migrationBuilder.DropColumn(
                name: "Directors",
                table: "Movies");

            migrationBuilder.DropColumn(
                name: "Stars",
                table: "Movies");
        }
    }
}
