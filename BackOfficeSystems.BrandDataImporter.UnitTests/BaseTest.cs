﻿using Serilog;
using Serilog.Events;

namespace BackOfficeSystems.BrandDataImporter.UnitTests
{
    public class BaseTest
    {
        static BaseTest()
        {
            Log.Logger =
                new LoggerConfiguration()
                    .Enrich.FromLogContext()
                    .WriteTo.Console(LogEventLevel.Information)
                    .CreateLogger();
        }
    }
}