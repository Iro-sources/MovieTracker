using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BackgroundServices.Migrations
{
    public partial class ComingSoonMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Movies",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    FullTitle = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Year = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ReleaseState = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Image = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RuntimeMins = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RuntimeStr = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Plot = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ContentRating = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IMDbRating = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IMDbRatingCount = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MetacriticRating = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Genres = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Movies", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Movies");
        }
    }
}
