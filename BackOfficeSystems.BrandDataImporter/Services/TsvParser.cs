﻿using System.Collections.Generic;
using System.IO;
using BackOfficeSystems.BrandDataImporter.Contracts;
using BackOfficeSystems.BrandDataImporter.Exceptions;
using BackOfficeSystems.BrandDataImporter.Models;
using NReco.Csv;
using Serilog;

namespace BackOfficeSystems.BrandDataImporter.Services
{
    public class TsvParser : ITsvParser
    {
        private string[] ReadRow(CsvReader reader)
        {
            var row = new string[reader.FieldsCount];
            for (var i = 0; i < reader.FieldsCount; i++)
            {
                row[i] = reader[i];
            }

            return row;
        }

        public TsvFile FromStream(string shortFileName, Stream stream)
        {
            using var textReader = new StreamReader(stream);
            var csvReader = new CsvReader(textReader, "\t");

            if (!csvReader.Read())
            {
                Log.Error("At least header row must exists in TSV file");
                throw new TsvParsingException("At least header row must exists in TSV file");
            }

            var headers = ReadRow(csvReader);
            var rows = new List<string[]>();

            while (csvReader.Read())
            {
                rows.Add(ReadRow(csvReader));
            }

            return new TsvFile
            {
                ShortName = shortFileName,
                Headers = headers,
                Rows = rows.ToArray()
            };
        }
    }
}