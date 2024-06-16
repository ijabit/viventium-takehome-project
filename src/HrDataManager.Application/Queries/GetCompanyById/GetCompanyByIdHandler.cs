using HrDataManager.Application.Dtos;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace HrDataManager.Application.Queries.GetCompanyById
{
    public class GetCompanyByIdHandler : IRequestHandler<GetCompanyByIdQuery, Company>
    {
        private readonly IHrDataDbContext _context;

        public GetCompanyByIdHandler(IHrDataDbContext context)
        {
            _context = context;
        }

        public async Task<Company> Handle(GetCompanyByIdQuery request, CancellationToken cancellationToken)
        {
            var company = await _context.Companies
                .Include(c => c.Employees)
                .FirstOrDefaultAsync(c => c.Id == request.Id, cancellationToken);

            if (company == null)
            {
                return null;
            }

            return new Company
            {
                Id = company.Id,
                Code = company.Code,
                Description = company.Description,
                EmployeeCount = company.Employees.Count,
                Employees = company.Employees.Select(e => new EmployeeHeader
                {
                    EmployeeNumber = e.EmployeeNumber,
                    FullName = $"{e.FirstName} {e.LastName}"
                }).ToArray()
            };
        }
    }
}
