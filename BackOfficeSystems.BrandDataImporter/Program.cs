﻿using System;
using BackOfficeSystems.BrandDataImporter.Configuration;
using BackOfficeSystems.BrandDataImporter.Exceptions;
using BackOfficeSystems.BrandDataImporter.Services;
using Microsoft.Extensions.Configuration;
using Serilog;
using Serilog.Events;

namespace BackOfficeSystems.BrandDataImporter
{
    class Program
    {
        static void ExecuteFlow(AppSettings settings, string connectionString)
        {
            var parser = new TsvParser();
            var validator = new TsvFileValidator();
            using var fillService = new DatabaseImportService(connectionString);
            var loader = new TsvLoader(parser, validator, fillService);

            loader.Load(settings.FilesToImport, settings.DbSchemaFile, settings.DatabaseName);
        }

        static void Main(string[] args)
        {
            var configuration =
                new ConfigurationBuilder()
                    .AddJsonFile("appsettings.json", true)
                    .AddEnvironmentVariables()
                    .Build();

            var config = configuration.Get<AppSettings>();

            Log.Logger =
                new LoggerConfiguration()
                    .WriteTo.Console(config.VerboseLogging ? LogEventLevel.Information : LogEventLevel.Warning)
                    .CreateLogger();

            var mySqlConnectionString = configuration.GetConnectionString("MySql");

            try
            {
                ExecuteFlow(config, mySqlConnectionString);
            }
            catch (TsvParsingException ex)
            {
                Log.Error("Error while parsing tsv file: {Error}", ex.Error);
            }
            catch (TsvValidationException ex)
            {
                Log.Error("Error while validation tsv file: {Error}", ex.Error);
            }
            catch (Exception ex)
            {
                Log.Fatal("Unknown error occured: {Error}", ex);
            }
        }
    }
}