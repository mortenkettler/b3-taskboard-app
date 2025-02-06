## Required User Stories
IMPLEMENTED:
1. As a user, I can view all lists and tasks which have been created
2. As a user, I can create an empty list with a name property
3. As a user, I can add new tasks to an existing list with a name and description
   property.
4. As a user, I can update the name and description of a tasks within a list
5. As a user, I can delete a task from a list
6. As a user, I can delete an entire list with all its tasks

NOT IMPLEMENTED YET: <br>
7. As a user, I can move tasks to a different list (not implemented)

## Requirements for Onboarding

The user needs to have installed .Net 8 SDK, an IDE (VS Code [recommended]
or VS2022 [not tested]).

Sometimes projects are not directly compatible between VSC and VS2022. I actually dont know why.
Git and Docker [Desktop] are needed for versioning and local testing of the container.
Docker is not needed for development per se (i.e., the user can run the app on their local
machine for testing).

How to check the SQLite database? I would recommend DB Browser, add database by adding the .db-file. I should use a more mature database anyhow for further development.

## Set up the Environment

- Clone the repository
- Run "dotnet restore"
- Install EF Core "dotnet tool install --global dotnet-ef --version 8.*"
- Should not be necessary as we distribute the Migrations: 
  * Run initial migrations and update the database:
  * "dotnet ef migrations add InitialCreate --project TaskBoard.Infrastructure --startup-project TaskBoard.Api"
- "dotnet ef database update --project TaskBoard.Infrastructure --startup-project TaskBoard.Api"
- RUn the application via "dotnet run --project TaskBoard.Api"

ADDENDUM: When testing docker locally (see below for persistent database with SQLite)
- start Docker
- run "docker build -t taskboard:latest ." to build the image 
- run "docker run -d -p 5000:80 --name taskboard_container taskboard:latest" to start the container
- check running containers with "docker ps"
--> there is a hot reload for .net 8: use Dockerfile.dev and "dotnet watch": [NOT IMPLEMENTED YET!]

# Run the Test(s):
- run from main directory "dotnet test ./TaskBoard.Tests/TaskBoard.Tests.csproj"

## Application Background

The application is a small demonstrator to show how a DDD-oriented microservice
could be built. It is to be run in a docker container, locally for demonstration
purposes.
Domain-driven design shall ensure that there is appropraite separation of concerns,
with:
- Domain Layer responsible for entities that are independently defined.
- Application Layer takes ccare of service interfaces and DTOs. Why DTOs? We do not
need to send around the whole entity, with internal data (i.e., IDs and other data).
- Infrastructure Layer handles communication with database via EF Core.
- API Layer# exposes the endpoints to the user and deals with HTTP requests. Here, we also
store info on appsettings, and the SQLite databse as a .db-file.

# Change Database:

- replace the SQLite in EF Core and update packages with different database
- update connection string in appsettings.json
- replace the DI in Program.cs
- test migrations and test i/o

# Persistent Database with SQLite on a Docker Volume
Due to time constraints, I will not introduce a prod-env with Postgresql, instead we can
use named volumes for persistent SQLite on the container. 
To test the environment do this:
- "docker run -d -p 8080:8080 --name taskboard_container -v taskboard_data:/app taskboard:latest"
- ADD some data via Insomnia/Postman
- "docker stop taskboard_container"
- "docker start taskboard_container"
- GET some data via Insomnia/Postman

# Next Steps in our Microservice PoC
- Create actually persistent database (Azure?)
- Take care of secrets when using prod-db (Github, Docker secrets etc.)
- CI/CD via Github actions
- Run the container in Azure
