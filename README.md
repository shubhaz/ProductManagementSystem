# ProductManagementSystem
Asp.net core Web API project

How to Run the Project
1. Clone Repository
git clone https://github.com/shubhaz/ProductManagementSystem.git


2. Navigate to Project
cd ProductManagementSystem

3. Update Connection String

Modify:

ProductManagementSystem.API/appsettings.json

Example:

"ConnectionStrings": {
  "DefaultConnection": "Server=YOUR_SERVER;Database=ProductManagementSystemDB;Trusted_Connection=True;TrustServerCertificate=True;"
}


4. Run Migrations
dotnet ef database update --project ProductManagementSystem.Infrastructure --startup-project ProductManagementSystem.API

5. Run Application
dotnet run --project ProductManagementSystem.API

6. Open Swagger
https://localhost:<port>/swagger


Test Execution

Run all tests:

dotnet test

Expected Result:

Passed: 8
Failed: 0
Skipped: 0
Docker Execution

Build and Run:

docker compose up --build

Access:

http://localhost:8080/swagger

7. Output of this project saved in Screenshot folder.
Drive:\ProductManagementSystem\Screenshots



