# Library-Management-System
An interview assessment project that allows users to create, retrieve, update, and delete books in a library system.

# Prerequisites
Before you begin, ensure you have the following installed:

.NET 8.0 SDK (or your specific version)

SQL Server (Express or LocalDB)

Visual Studio 2022 or VS Code

# installation & Setup

1 Clone the repository
 gitbash
git clone https://github.com/your-username/LibraryManagementSystem.git

 cd LibraryManagementSystem
2 Configure the Database Open LibraryManagementSystems.Api/appsettings.json and update the connection string to point to your SQL Server instance:

JSON
"ConnectionStrings": {
  "DefaultConnection": "Server=(localdb)\\mssqllocaldb;Database=LibraryDb;Trusted_Connection=True;MultipleActiveResultSets=true"
}

3 Apply Migrations Open the Package Manager Console in Visual Studio and run:
  
    Add-Migration migrationName
    Update-Database

4 Run the Application Press F5 in Visual Studio or run the following command:
   
   dotnet run --project LibraryManagementSystems.Api


 # Authentication Flow
  Login: Use the Auth endpoints to get a JWT token.

  Authorize: Click the "Authorize" button in Swagger.

  Token: Paste your token. (The "Bearer " prefix is handled automatically in the current configuration).
