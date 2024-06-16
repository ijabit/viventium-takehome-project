using HrDataManager.Application.Dtos;
using MediatR;
using System.Collections.Generic;

namespace HrDataManager.Application.Queries.GetCompanies
{
    public class GetCompaniesQuery : IRequest<IEnumerable<CompanyHeader>>
    {
    }
}
