using HrDataManager.Application.Dtos;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace HrDataManager.Application.Queries.GetCompanies
{
    public class GetCompaniesHandler : IRequestHandler<GetCompaniesQuery, IEnumerable<CompanyHeader>>
    {
        private readonly IHrDataDbContext _context;

        public GetCompaniesHandler(IHrDataDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<CompanyHeader>> Handle(GetCompaniesQuery request, CancellationToken cancellationToken)
        {
            return await _context.Companies
                .Select(c => new CompanyHeader
                {
                    Id = c.Id,
                    Code = c.Code,
                    Description = c.Description,
                    EmployeeCount = c.Employees.Count
                })
                .ToListAsync(cancellationToken);
        }
    }
}
