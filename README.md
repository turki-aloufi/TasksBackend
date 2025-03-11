
# Todo API (ASP.NET Core Backend)

This is an ASP.NET Core Web API that provides CRUD operations for a todo list, using Entity Framework Core with an in-memory database. It serves as the backend for the Angular Todo App.

## Features
- RESTful endpoints for managing tasks (GET, POST, PUT, DELETE).
- In-memory database for simplicity (can be swapped for SQL Server or other providers).
- CORS enabled for Angular frontend integration.

## Prerequisites
- .NET SDK (v6.0 or later recommended)
- Visual Studio or VS Code (optional)

## Project Structure
- TasksBackend/
  - Controllers/
    - TaskController.cs     # API endpoints for CRUD
  - Data/
    - AppDbContext.cs       # EF Core DbContext
  - Models/
    - TodoTask.cs           # Task model
  - Program.cs             # Application entry point with CORS and services
  - TasksBackend.csproj    # Project file

## Setup Instructions
1. **Navigate to Project Directory:**
   ```powershell
   cd C:\Users\turki\source\repos\TasksBackend
   ```
   - Ensure a `.csproj` file exists. If not, create a new project:
     ```powershell
     dotnet new webapi -o TasksBackend
     cd TasksBackend
     ```

2. **Restore Dependencies:**
   ```powershell
   dotnet restore
   ```

3. **Run the Application:**
   ```powershell
   dotnet run
   ```
   - Output should show:
     ```
     Now listening on: http://localhost:7260
     ```
   - API is accessible at `http://localhost:7260/api/task`.

## API Endpoints
- **GET /api/task** - Retrieve all tasks
- **GET /api/task/{id}** - Retrieve a task by ID
- **POST /api/task** - Create a new task
  - Body: `{ "title": "string", "description": "string", "completed": boolean }`
- **PUT /api/task/{id}** - Update a task
  - Body: `{ "taskId": "string", "title": "string", "description": "string", "completed": boolean }`
- **DELETE /api/task/{id}** - Delete a task

## Configuration
- **CORS:** Enabled in `Program.cs` to allow requests from any origin:
  ```csharp
  builder.Services.AddCors(options =>
  {
      options.AddPolicy("AllowAll", policy =>
      {
          policy.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
      });
  });
  app.UseCors("AllowAll");
  ```
  - Update to `WithOrigins("http://localhost:4200")` for production to restrict to Angular.

- **Database:** Uses in-memory database by default:
  ```csharp
  builder.Services.AddDbContext<AppDbContext>(options =>
      options.UseInMemoryDatabase("TasksDb"));
  ```
  - Replace with a persistent database (e.g., SQL Server) if needed.

## Dependencies
- `Microsoft.EntityFrameworkCore` - ORM for database access.
- `Microsoft.EntityFrameworkCore.InMemory` - In-memory database provider.
- `Microsoft.AspNetCore.Mvc` - Web API framework.

## Troubleshooting
- **Project Not Found:** Ensure you’re in the directory with `TasksBackend.csproj`. Use `dir` to verify.
- **Port Conflict:** Check `launchSettings.json` or change the port if `7260` is in use.
- **CORS Issues:** Verify `UseCors` is before `UseAuthorization` in `Program.cs`.

## Testing
- Use Postman or a browser to test `http://localhost:7260/api/task`.
- Pair with the Angular frontend for full functionality.
