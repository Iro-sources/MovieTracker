How to Run the MovieTrackerSolution Project
Follow these steps to set up and run the MovieTrackerSolution project:


1. Open the "MovieTrackerSolution" project in Visual Studio.
2. (This step must be done if one is not using the Visual Studio IDE) Make sure you have one
running SQL server on your machine, and enter "server-name" in the appsettings.json file below
the variable "server".
Example:
"ConnectionStrings": {
"UserMovieData": "Server={your server name here}; Database=UserMovieData;
Trusted_Connection=True; TrustServerCertificate=True"
}
This process must be repeated for the projects/microservices "UserManagement.Api" ,
"UserMoviesGrpcService", "UserMoviesData.Api" and "BackgroundService".
3. You must then run the migration script located in the "Migrations" folder for both
"UserManagement.Api" , "UserMoviesGrpcService", "UserMoviesData.Api" and
“Background Service”.
Open the "Package Manager Console", set the project as "Set as Starter Project" or select i
default project menu.
Type "update-database" and run the command.
4. Right-click on the "MovieTrackerSolution" solution in the Solution Explorer and select "Set Startup
Projects". Select "Multiple startup projects". In the Action column of the table, select "Start" for all
projects except "Auth.Models" and "MovieTracker.Models".
5. Run the project
