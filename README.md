# JWTAuthAPI.CleanArchitecture


## Changes have been made to the architecture design of the JWTAuthAPI project, emphasizing separation of concerns and clean architecture principles. 
The architecture now consists of four main components - Core, Infrastructure, API, and Tests - each with its own specific role.

* Application.Core: This component holds the business model, including entities, services, and interfaces. It is independent of other projects, making it a self-contained module.
* Application.Infrastructure: This component implements the abstraction from the Core component for data and file access. It includes repository implementations, file services, and context setup for EF Core, among other things. All data access, including migrations, is implemented here.
* Application.API: This component is the entry point of the application, serving as the gateway where the client connects. It holds launch and startup information and API endpoints.
* Application.Test: This component houses the project's test cases.

These components are designed to work together seamlessly while being independently deployable, promoting modularity and maintainability of the codebase.

Finally, all models have been abstracted under the BaseEntity, following the Abstraction Pattern, promoting modularity and improving maintainability of the codebase.


## Other Features
* JWT Authentication: Implements a secure authentication system with JSON Web Tokens (JWT) for enhanced security.
* Entity Framework: Uses Entity Framework to simplify data access and manipulate data from the database.
* Specification Pattern: Uses the specification pattern to query the database with complex criteria to retrieve entities that match the criteria.
* Service/Repository/Controller pattern: Uses the Service/Repository/Controller pattern to improve code organization and maintainability.
* Authorization Handler: Forbid users who are not owners from modifying resources to improve data security.
* Database: Currently using InMemoryDatabase, but you can easily swap with the SQL Server by setting the appsettings.json {"UseInMemoryDatabase" : false}
* FluentValidation : Uses the fluent validators for request model validation

## Usage
This API provides the following endpoints:

* POST /api/v1/accounts/login
* POST /api/v1/accounts/register

[Secure Endpoints]
* GET /api/v1/accounts/users/id
* PUT /api/v1/accounts/users/id
* DELETE /api/v1/accounts/users/id 

* POST /api/v1/products/
* GET /api/v1/products/id
* PUT /api/v1/products/id
* DELETE /api/v1/products/id 

[Admin Role Needed]
* GET /api/v1/accounts/users 

You can use the following HTTP methods to interact with these endpoints:

* GET: retrieve data
* POST: create new data
* PUT: update existing data
* DELETE: delete data

Make sure to include a valid JWT token in the Authorization header when making requests to the Secure Endpoints. You will receive the token once you have registered the user and have logged in!