﻿using Serilog;
using Serilog.Events;

namespace BackOfficeSystems.BrandDataImporter.UnitTests
{
    /// <summary>
    /// Base class for every test. Must be used when logging need to be used
    /// </summary>
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