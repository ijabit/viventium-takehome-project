using HrDataManager.Application.Dtos;
using HrDataManager.Application.Queries.GetCompanyEmployee;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace HrDataManager.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmployeeController : ControllerBase
    {
        private readonly IMediator _mediator;

        public EmployeeController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("{companyId}/{employeeNumber}")]
        public async Task<ActionResult<Employee>> GetEmployee(int companyId, string employeeNumber)
        {
            var employee = await _mediator.Send(new GetEmployeeByCompanyIdEmployeeIdQuery { CompanyId = companyId, EmployeeNumber = employeeNumber });
            if (employee == null)
            {
                return NotFound();
            }
            return Ok(employee);
        }
    }
}
