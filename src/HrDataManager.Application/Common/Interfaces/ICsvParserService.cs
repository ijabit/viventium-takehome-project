using HrDataManager.Application.DTOs;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace HrDataManager.Application.Common.Interfaces
{
    public interface ICsvParserService
    {
        Task<List<CompanyEmployeeDataRecord>> ParseCsvAsync(Stream csvStream);
    }
}
