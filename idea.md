“Enterprise Task Processing Backend”
1. receives tasks via REST API
2. processes them asynchronously
3. stores status in database
4. exposes monitoring endpoint

Needed:
1. async processing
2. queues / background workers
3. database state (task CRUD)
4. REST API design
5. user registration/login (auth JWT, Roles, departments)
6. Error handling
7. Filtering
8. Pagination
9. Basic reporting endpoints

Minimum stack

C# + ASP.NET Core
Docker
BackgroundService worker
OpenAPI (Swashbuckle.AspNetCore)
DB: Microsoft.EntityFrameworkCore.Sqlite
Auth: Microsoft.AspNetCore.Authentication.JwtBearer