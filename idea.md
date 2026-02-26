“Enterprise Task Processing Backend”
1. receives tasks via REST API
2. processes them asynchronously
3. stores status in database
4. exposes monitoring endpoint

Needed:
1. async processing
2. queues / background workers
3. database state
4. API design
5. architecture thinking

Minimum stack

C# + ASP.NET Core
PostgreSQL
Docker
BackgroundService worker
OpenAPI

Example tasks:
1. document conversion
2. report generation
3. email sending
4. data parsing