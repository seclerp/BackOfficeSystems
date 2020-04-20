using System.IO;
using System.Text;
using BackOfficeSystems.BrandDataImporter.Exceptions;
using BackOfficeSystems.BrandDataImporter.Services;
using Xunit;

namespace BackOfficeSystems.BrandDataImporter.UnitTests
{
    public class TsvParserTests : BaseTest
    {
        private static Stream StringToStream(string input)
        {
            var bytes = Encoding.ASCII.GetBytes(input);
            return new MemoryStream(bytes);
        }

        [Fact]
        public void Parse_Valid_Tsv_Input()
        {
            var shortName = "ValidFile";
            using var validTsvStream = StringToStream("Id\tName\n\n123\t321");
            var expectedHeaders = new [] { "Id", "Name" };
            var expectedRows = new [] { new [] {"123", "321"} };

            var tsvParser = new TsvParser();
            var file = tsvParser.FromStream(shortName, validTsvStream);

            Assert.Equal(file.Headers, expectedHeaders);
            Assert.Equal(file.Rows.Length, expectedRows.Length);
            Assert.Equal(file.Rows[0], expectedRows[0]);
        }

        [Fact]
        public void Parse_Invalid_Tsv_Input()
        {
            var shortName = "InvalidFile";
            using var validTsvStream = StringToStream("");
            var tsvParser = new TsvParser();

            void Act() => tsvParser.FromStream(shortName, validTsvStream);

            Assert.Throws<TsvParsingException>(Act);
        }
    }
}