using HrDataManager.Application.Dtos;
using HrDataManager.Application.Queries.GetCompanies;
using HrDataManager.Application.Queries.GetCompanyById;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HrDataManager.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CompanyController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CompanyController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<ActionResult<List<CompanyHeader>>> GetCompanies()
        {
            var companies = await _mediator.Send(new GetCompaniesQuery());
            return Ok(companies);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Company>> GetCompany(int id)
        {
            var company = await _mediator.Send(new GetCompanyByIdQuery { Id = id });
            if (company == null)
            {
                return NotFound();
            }
            return Ok(company);
        }
    }
}
