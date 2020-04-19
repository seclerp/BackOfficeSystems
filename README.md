# BackOfficeSystems
Mini project implementation for Senior Software Engineer position

## Requirements
You need to have **.NET Core 3.1** installed in order to run this solution.

## Part One

### Overview
Part one is implemented as a console app named `BackOfficeSystems.BrandDataImporter`. It can consume settings from environment variables and `appsettings.json` file.

Application need to know how tables are looking. So it need to use SQL schema for it. Schema for Brands are in `DbSchema.sql` file and is used by default.

In current implementation there is 1:1 relation between file and table. So, only 1 .tsv per table could be used in one run.

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