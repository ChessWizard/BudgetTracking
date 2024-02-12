using BudgetTracking.Core.File.Enums;
using BudgetTracking.Data.Dto;
using BudgetTracking.Service.Services.File.Commands.ExportFile;
using BudgetTracking.Service.Services.File.Commands.ExportTransaction;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BudgetTracking.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FilesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public FilesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("export/transaction")]
        [Authorize]
        public async Task<IActionResult> ExportTransactionFile([FromBody] ExportTransactionCommand command)
        {
            var result = await _mediator.Send(command);

            // Download edilebilir dosya linki return edilir
            return File(result.Item1, result.Item2 , result.Item3);
        }
    }
}
