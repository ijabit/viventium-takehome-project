using HrDataManager.Application.Common.Exceptions;
using HrDataManager.Domain.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace HrDataManager.Application.Commands.UpdateDataStore
{
    public class UpdateDataStoreCommandHandler : IRequestHandler<UpdateDataStoreCommand>
    {
        private readonly IHrDataDbContext _context;

        public UpdateDataStoreCommandHandler(IHrDataDbContext context)
        {
            _context = context;
        }

        public async Task Handle(UpdateDataStoreCommand request, CancellationToken cancellationToken)
        {
            // Convert flat data to nested Company/Employee objects
            var companies = new List<Company>();

            foreach (var record in request.ParsedCompanyEmployeeData)
            {
                var company = companies.FirstOrDefault(c => c.Id == record.CompanyId);
                if (company == null)
                {
                    company = new Company
                    {
                        Id = record.CompanyId,
                        Code = record.CompanyCode,
                        Description = record.CompanyDescription,
                        Employees = new List<Employee>()
                    };
                    companies.Add(company);
                }

                // Check for unique employee number within the company
                if (company.Employees.Any(e => e.EmployeeNumber == record.EmployeeNumber))
                {
                    throw new BusinessRuleValidationException($"Employee number {record.EmployeeNumber} is not unique within company {record.CompanyCode}.");
                }

                // Validate manager existence within the same company before adding the employee
                if (!string.IsNullOrEmpty(record.ManagerEmployeeNumber))
                {
                    var managerRecord = request.ParsedCompanyEmployeeData.FirstOrDefault(r => r.EmployeeNumber == record.ManagerEmployeeNumber && r.CompanyId == record.CompanyId);
                    if (managerRecord == null)
                    {
                        throw new BusinessRuleValidationException($"Manager with employee number {record.ManagerEmployeeNumber} does not exist in company {record.CompanyCode}.");
                    }
                }

                company.Employees.Add(new Employee
                {
                    EmployeeNumber = record.EmployeeNumber,
                    FirstName = record.EmployeeFirstName,
                    LastName = record.EmployeeLastName,
                    Email = record.EmployeeEmail,
                    Department = record.EmployeeDepartment,
                    HireDate = record.HireDate,
                    ManagerEmployeeNumber = record.ManagerEmployeeNumber,
                    CompanyId = record.CompanyId
                });
            }

            using var transaction = await _context.BeginTransactionAsync(cancellationToken);
            try
            {
                await _context.Companies.ExecuteDeleteAsync(cancellationToken);
                await _context.Employees.ExecuteDeleteAsync(cancellationToken);

                _context.Companies.AddRange(companies);
                await _context.SaveChangesAsync(cancellationToken);

                transaction.Commit();
            }
            catch (Exception)
            {
                transaction.Rollback();
                throw;
            }
        }
    }
}
