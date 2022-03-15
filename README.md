# Wizard

.Net + Angular application, "Simple registration wizard"

### How to run
To debug application in VS or Rider, just hit `F5`, it will start backend service, and frontend via
SPAProxy.

To test application in "production" mode (release assemblies, static files, containerized)
 run `docker-compose up` in solution root, docker daemon should be installed and started.

### Used technologies
- .Net 6
- EFCore (code-first) (Sqlite for development, Postgres for "production")
- MediatR
- Swagger

### backend endpoints
backend service provides following endpoints:
- `GET v1/countries` : retrieves all countries list
- `GET v1/provinces/{id}` : retrieves country provinces by country id
- `POST add-registration` : adds new registration

### Settings
You can set up backend service settings through appsettings.json, appsettings.{environment}.json,
or with environment variables. Settings are applies in following order:
- appsettings.json
- appsettings.{environment}.json
- environment variables

### Settings description
| setting name                  | description                                              |
|-------------------------------|----------------------------------------------------------|
| ConnectionStrings__Database   | database connection string                               |
| DatabaseProvider              | database provider value (sqlite/postgres)                |
| MigrationsAssembly            | migration assembly name (differs for database providers) |

