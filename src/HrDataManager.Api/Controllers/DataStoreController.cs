using HrDataManager.Application.Commands.UpdateDataStore;
using HrDataManager.Application.Common.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace HrDataManager.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DataStoreController : ControllerBase
    {
        private readonly ICsvParserService _csvParserService;
        private readonly IMediator _mediator;

        public DataStoreController(ICsvParserService csvParserService, IMediator mediator)
        {
            _csvParserService = csvParserService;
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> UploadData(IFormFile file)
        {
            if (file == null || file.Length == 0)
            {
                return BadRequest("No file uploaded.");
            }

            // Deserialization of CSV data a responsibility of the API
            var parsedCompanyEmployeeData = await _csvParserService.ParseCsvAsync(file.OpenReadStream());

            // Processing of the updated data a responsibility of the Application
            await _mediator.Send(new UpdateDataStoreCommand { ParsedCompanyEmployeeData = parsedCompanyEmployeeData });

            return Ok();
        }
    }
}
