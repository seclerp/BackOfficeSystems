# BackOfficeSystems
Mini project implementation for Senior Software Engineer position

## Requirements
You need to have **.NET Core 3.1** installed in order to run this solution.

## Part One

### Overview
Part one is implemented as a console app named `BackOfficeSystems.BrandDataImporter`.

Main point about implementation is that is generic. You need to only change couple of files to add new tables, files or change schema.

Application need to know how tables are looking to create it on fly. So it need to use SQL schema for it. Schema for Brands are in `DbSchema.sql` file and is used by default.

In current implementation there is 1:1 relation between file and table. So, only one `.tsv` per SQL table could be used in one run.

Also, not all data could be inserted in MySQL database as is. For example, datetime need to be converted to appropriate view to avoid MySQL errors. For that purpose application will try to get column types from database and transform data using one of `ITransformer` implementation.

Currently implemented only custom ITransformer only for datetime.

There is **Serilog** logging added to a project. It then could be used to setup Splunk, Grafana, etc. alerts and monitoring, if use this application as scheduled service.

General flow:
1. Resolve file contents by filenames
2. Validate files
3. Load scheme
4. Ensure that database for insertion exists, create if not
5. Insert data from files into tables

Application can consume settings from environment variables and `appsettings.json` file (env variables have more priority that file settings).

Also, application is cross platform and could be started on all systems supported by .NET Core 3.1 (even on Docker, for example)

Used NuGet packages:
- `Dapper`
- `MySql.Data`
- `NReco.Csv`
- `Serilog`
- `Serilog.Sinks.Console`
- `Microsoft.Extensions.Configuration.Binder`
- `Microsoft.Extensions.Configuration.EnvironmentVariables`
- `Microsoft.Extensions.Configuration.Json`

### Running
To run application execute following in console/terminal (from the root repository folder):

Windows:
```cmd
set ConnectionStrings__MySql=YOUR_CONFIGURATION_STRING_HERE
cd BackOfficeSystems.BrandDataImporter
dotnet run
```

Unix:
```bash
export ConnectionStrings__MySql=YOUR_CONFIGURATION_STRING_HERE
cd BackOfficeSystems.BrandDataImporter
dotnet run
```

IDE:

Just open .sln file in your favorite IDE, provide `ConnectionStrings__MySql` env variable to `BackOfficeSystems.BrandDataImporter` build configuration and hit `"Build and Run"`.