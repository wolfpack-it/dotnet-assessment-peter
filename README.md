# Wolfpack API
Wolfpack.API is a REST API written in ASP.net Core and .NET 6 for managing Packs.

## Getting started
To get the API up and running, you can either do this through docker-compose or through visual studio.
The easiest way is through docker-compose.

Installing docker-compose for Windows or MacOs should already be done if you install docker-desktop - the docs for installing are found [here](https://docs.docker.com/compose/install/).

Then getting it up and running is only a matter of calling
```
docker-compose up -d --build
```

When the API is running through the visual studio debugger/`dotnet run`, it is reachable at https://localhost:5001. When run through Docker (see the Docker section), it is reachable at http://host.docker.internal:80.

## OpenAPI & Swagger
Swagger docs can be accessed at https://localhost:5001/swagger/index.html (local debugging) or http://host.docker.internal:80/swagger/index.html (Docker).

We've also provided an openAPI file for the expected result, this is the file wolfpack-assessment-openapi.yml - this should be viewable with any OpenAPI/Swagger previewer.

## Used Libraries
Several NuGet libraries are used in the API:
1. Entity Framework Core
2. Fluent Validation
3. AutoMapper

### Swagger
Swagger docs can be accessed at https://localhost:5001/swagger/index.html (local debugging) or http://host.docker.internal:80/swagger/index.html (Docker).

### Run with docker (compose)
The repository contains a `docker-compose.yml` which contains the API and SQL server container.

Run it by executing:
```
docker-compose up -d --build
```
Note that the `--build` flag should be supplied. If this is not done, `docker-compose up` will re-use the latest build and changes made to the project will not be reflected.

### Configuration
Configuration is stored in the `appsettings.json` file in the `Wolfpack.API` project. Currently only the connection string to the SQL server database is configured.

### Migrations
The Migrations feature enables you to change the data model and deploy your changes to production by updating the database schema without having to drop and re-create the database.

To create a new migration (after entities are updated) run 
```
dotnet ef migrations add MyNewMigration -p Wolfpack.Data.Database -s Wolfpack.Api
```
or
```
Add-Migration MyNewMigration -Project Wolfpack.Data.Database -StartupProject Wolfpack.Api
```
The database is migrated automatically when the API starts.