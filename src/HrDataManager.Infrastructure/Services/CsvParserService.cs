using CsvHelper;
using HrDataManager.Application.Common.Interfaces;
using HrDataManager.Application.DTOs;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace HrDataManager.Infrastructure.Services
{
    public class CsvParserService : ICsvParserService
    {
        public async Task<List<CompanyEmployeeDataRecord>> ParseCsvAsync(Stream csvStream)
        {
            using var streamReader = new StreamReader(csvStream);
            using var csvReader = new CsvReader(streamReader, CultureInfo.InvariantCulture);

            var records = await csvReader.GetRecordsAsync<CompanyEmployeeDataRecord>().ToListAsync();

            return records;
        }
    }
}
