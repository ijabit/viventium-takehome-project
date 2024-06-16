using HrDataManager.Application.DTOs;
using MediatR;
using System.Collections.Generic;

namespace HrDataManager.Application.Commands.UpdateDataStore
{
    public class UpdateDataStoreCommand : IRequest
    {
        public List<CompanyEmployeeDataRecord> ParsedCompanyEmployeeData { get; set; }
    }
}
