using HrDataManager.Application.Dtos;
using HrDataManager.Domain.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace HrDataManager.Application.Queries.GetCompanyEmployee
{
    public class GetEmployeeByCompanyIdEmployeeIdHandler : IRequestHandler<GetEmployeeByCompanyIdEmployeeIdQuery, Dtos.Employee>
    {
        private readonly IHrDataDbContext _context;

        public GetEmployeeByCompanyIdEmployeeIdHandler(IHrDataDbContext context)
        {
            _context = context;
        }

        public async Task<Dtos.Employee> Handle(GetEmployeeByCompanyIdEmployeeIdQuery request, CancellationToken cancellationToken)
        {
            var employee = await _context.Employees
                .Include(e => e.Company)
                .FirstOrDefaultAsync(e => e.CompanyId == request.CompanyId && e.EmployeeNumber == request.EmployeeNumber, cancellationToken);

            if (employee == null)
            {
                return null;
            }

            return new Dtos.Employee
            {
                EmployeeNumber = employee.EmployeeNumber,
                FullName = $"{employee.FirstName} {employee.LastName}",
                Email = employee.Email,
                Department = employee.Department,
                HireDate = employee.HireDate,
                Managers = (await GetManagersAsync(employee.ManagerEmployeeNumber, cancellationToken)).ToArray()
            };
        }

        /// <summary>
        /// If this wasn't sqlite I'd use a recursive table-valued function all EF to access it via .HasDbFunction() for better efficiency.
        /// However, since the query is retrieving only one employee at a time and the hierarchy is unlikely to be more than a few levels deep, performance is acceptable here.
        /// </summary>
        private async Task<List<EmployeeHeader>> GetManagersAsync(string managerEmployeeNumber, CancellationToken cancellationToken)
        {
            var managers = new List<EmployeeHeader>();

            while (!string.IsNullOrEmpty(managerEmployeeNumber))
            {
                var manager = await _context.Employees
                    .Where(e => e.EmployeeNumber == managerEmployeeNumber)
                    .Select(e => new EmployeeHeader
                    {
                        EmployeeNumber = e.EmployeeNumber,
                        FullName = $"{e.FirstName} {e.LastName}"
                    })
                    .FirstOrDefaultAsync(cancellationToken);

                if (manager == null)
                {
                    break;
                }

                managers.Add(manager);

                var nextManager = await _context.Employees
                    .Where(e => e.EmployeeNumber == manager.EmployeeNumber)
                    .Select(e => e.ManagerEmployeeNumber)
                    .FirstOrDefaultAsync(cancellationToken);

                managerEmployeeNumber = nextManager;
            }

            return managers;
        }
    }
}
