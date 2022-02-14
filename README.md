# Wolfpack API
Wolfpack.API is a REST API written in ASP.net Core and .NET 6 for managing Packs.

Several NuGet libraries are used in the API:
1. Entity Framework Core
2. Fluent Validation
3. AutoMapper

## Getting started
When the API is running through the visual studio debugger/`dotnet run`, it is reachable at https://localhost:5001. When run through Docker (see the Docker section), it is reachable at http://host.docker.internal:80.

### Swagger
Swagger docs can be accessed at https://localhost:5001/swagger/index.html (local debugging) or http://host.docker.internal:80/swagger/index.html (Docker).

### Configuration
Configuration is stored in the `appsettings.json` file in the `Wolfpack.API` project. Currently only the connection string to the SQL server database is configured.

### Run with docker (compose)
The repository contains a `docker-compose.yml` which contains the API and SQL server container.

Run it by executing:
```
docker-compose up -d --build
```
Note that the `--build` flag should be supplied. If this is not done, `docker-compose up` will re-use the latest build and changes made to the project will not be reflected.