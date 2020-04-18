using System;
using System.Collections.Generic;
using BackOfficeSystems.BrandDataImporter.Configuration;
using BackOfficeSystems.BrandDataImporter.Services;
using Microsoft.Extensions.Configuration;

namespace BackOfficeSystems.BrandDataImporter
{
    class Program
    {
        static void Main(string[] args)
        {
            var configuration =
                new ConfigurationBuilder()
                    .AddJsonFile("appsettings.json", true)
                    .AddEnvironmentVariables()
                    .Build();

            var config = configuration.Get<AppSettings>();
            var mySqlConnectionString = configuration.GetConnectionString("MySql");

            var mappingsLoader = new MappingsLoader();
            var mappings = mappingsLoader.FromFile(config.DbSchema.MappingFile);

            var parser = new TsvParser();
            parser.FromStream();

            Console.WriteLine("Hello World!");
        }
    }
}