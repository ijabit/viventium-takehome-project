using HrDataManager.Application.Dtos;
using MediatR;

namespace HrDataManager.Application.Queries.GetCompanyEmployee
{
    public class GetEmployeeByCompanyIdEmployeeIdQuery : IRequest<Employee>
    {
        public int CompanyId { get; set; }
        public string EmployeeNumber { get; set; }
    }
}
