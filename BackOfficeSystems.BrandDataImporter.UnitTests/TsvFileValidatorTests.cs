﻿using BackOfficeSystems.BrandDataImporter.Exceptions;
using BackOfficeSystems.BrandDataImporter.Models;
using BackOfficeSystems.BrandDataImporter.Services;
using Xunit;

namespace BackOfficeSystems.BrandDataImporter.UnitTests
{
    /// <summary>
    /// Unit tests for <see cref="TsvFileValidator"/> class
    /// </summary>
    public class TsvFileValidatorTests : BaseTest
    {
        [Fact]
        public void Validate_Valid_Tsv_File()
        {
            var validFile = new TsvFile(
                "ValidFile",
                new[] {"Id", "Name"},
                new[] {new[] {"123", "321"}, new[] {"567", "765"}}
            );
            var tsvValidator = new TsvFileValidator();

            tsvValidator.Validate(validFile);

            // No exceptions - test and validation successful, there is no Assert.Ok()
        }

        [Fact]
        public void Validate_Invalid_Tsv_File()
        {
            var invalidFile = new TsvFile(
                "InvalidFile",
                new[] {"Id"},
                new[] {new[] {"123", "321"}, new[] {"567", "765", "234234"}}
            );
            var tsvValidator = new TsvFileValidator();

            void Act() => tsvValidator.Validate(invalidFile);

            Assert.Throws<TsvValidationException>(Act);
        }
    }
}