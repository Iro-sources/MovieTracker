using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace UserManagment.Api.Migrations
{
    /// <inheritdoc />
    public partial class inital : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "MovieGenres",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MovieGenres", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Role = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UserGenreConfigs",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserGenreConfigs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserGenreConfigs_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ConfigMovieGenres",
                columns: table => new
                {
                    UserGenreConfigId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    MovieGenreId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ConfigMovieGenres", x => new { x.UserGenreConfigId, x.MovieGenreId });
                    table.ForeignKey(
                        name: "FK_ConfigMovieGenres_MovieGenres_MovieGenreId",
                        column: x => x.MovieGenreId,
                        principalTable: "MovieGenres",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ConfigMovieGenres_UserGenreConfigs_UserGenreConfigId",
                        column: x => x.UserGenreConfigId,
                        principalTable: "UserGenreConfigs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "MovieGenres",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Comedy" },
                    { 2, "Sci-Fi" },
                    { 3, "Horror" },
                    { 4, "Comedy-romance" },
                    { 5, "Documentary" },
                    { 6, "Romance" },
                    { 7, "Drama" },
                    { 8, "Animation" },
                    { 9, "Action-Comedy" },
                    { 10, "Family" },
                    { 11, "Action" },
                    { 12, "Mystery" },
                    { 13, "Adventure" },
                    { 14, "SuperHero" },
                    { 15, "History" },
                    { 16, "Thriller" },
                    { 17, "Crime" },
                    { 18, "Fantasy" },
                    { 19, "Western" }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "CreatedAt", "Email", "FirstName", "LastName", "Password", "Role" },
                values: new object[,]
                {
                    { "123e4567-e89b-12d3-a456-426655440000", new DateTime(2023, 2, 8, 2, 27, 29, 184, DateTimeKind.Local).AddTicks(4319), "philip.eiler@hotmail.com", "Philip", "Fleischer", "philip", "Master" },
                    { "5ae964bd-51c7-4cdb-b0f6-71216f59ee2b", new DateTime(2023, 2, 8, 2, 27, 29, 184, DateTimeKind.Local).AddTicks(4361), "mohamed@hotmail.com", "Mohamed", "Hassan", "mohamed", "User" },
                    { "6a2ddcb0-11fb-4c41-9402-9ad533b1db68", new DateTime(2023, 2, 8, 2, 27, 29, 184, DateTimeKind.Local).AddTicks(4355), "stine@hotmail.com", "Stine", "Kolsvik", "stine", "Admin" },
                    { "814e4bb6-52d5-4021-b689-0a152c8ea5b7", new DateTime(2023, 2, 8, 2, 27, 29, 184, DateTimeKind.Local).AddTicks(4358), "robin@hotmail.com", "Robin", "Dahlman", "robin", "User" }
                });

            migrationBuilder.InsertData(
                table: "UserGenreConfigs",
                columns: new[] { "Id", "UserId" },
                values: new object[,]
                {
                    { "6128e6a7-8012-4180-8ec1-84d52f63b1cc", "814e4bb6-52d5-4021-b689-0a152c8ea5b7" },
                    { "72ba74d0-6d21-4dbc-9e95-3a61902a39e3", "6a2ddcb0-11fb-4c41-9402-9ad533b1db68" },
                    { "93742ba8-ce40-4964-8ac9-8adc4690a671", "123e4567-e89b-12d3-a456-426655440000" },
                    { "b547a1af-78f8-4430-a9ee-e6752a533af9", "5ae964bd-51c7-4cdb-b0f6-71216f59ee2b" }
                });

            migrationBuilder.InsertData(
                table: "ConfigMovieGenres",
                columns: new[] { "MovieGenreId", "UserGenreConfigId" },
                values: new object[,]
                {
                    { 1, "6128e6a7-8012-4180-8ec1-84d52f63b1cc" },
                    { 3, "6128e6a7-8012-4180-8ec1-84d52f63b1cc" },
                    { 4, "6128e6a7-8012-4180-8ec1-84d52f63b1cc" },
                    { 12, "6128e6a7-8012-4180-8ec1-84d52f63b1cc" },
                    { 2, "72ba74d0-6d21-4dbc-9e95-3a61902a39e3" },
                    { 3, "72ba74d0-6d21-4dbc-9e95-3a61902a39e3" },
                    { 10, "72ba74d0-6d21-4dbc-9e95-3a61902a39e3" },
                    { 11, "72ba74d0-6d21-4dbc-9e95-3a61902a39e3" },
                    { 19, "72ba74d0-6d21-4dbc-9e95-3a61902a39e3" },
                    { 1, "93742ba8-ce40-4964-8ac9-8adc4690a671" },
                    { 2, "93742ba8-ce40-4964-8ac9-8adc4690a671" },
                    { 5, "93742ba8-ce40-4964-8ac9-8adc4690a671" },
                    { 10, "93742ba8-ce40-4964-8ac9-8adc4690a671" },
                    { 1, "b547a1af-78f8-4430-a9ee-e6752a533af9" },
                    { 2, "b547a1af-78f8-4430-a9ee-e6752a533af9" },
                    { 3, "b547a1af-78f8-4430-a9ee-e6752a533af9" },
                    { 17, "b547a1af-78f8-4430-a9ee-e6752a533af9" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_ConfigMovieGenres_MovieGenreId",
                table: "ConfigMovieGenres",
                column: "MovieGenreId");

            migrationBuilder.CreateIndex(
                name: "IX_UserGenreConfigs_UserId",
                table: "UserGenreConfigs",
                column: "UserId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_Email",
                table: "Users",
                column: "Email",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ConfigMovieGenres");

            migrationBuilder.DropTable(
                name: "MovieGenres");

            migrationBuilder.DropTable(
                name: "UserGenreConfigs");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
