using BudgetTracking.Common.Controller;
using BudgetTracking.Service.Enums;
using BudgetTracking.Service.Services.ExpenseEntity.Commands.CreateExpense;
using BudgetTracking.Service.Services.ExpenseEntity.Commands.SearchExpense;
using BudgetTracking.Service.Services.ExpenseEntity.Queries.GetExpensesByDate;
using BudgetTracking.Service.Services.ExpenseEntity.Queries.GetExpensesByUser;
using BudgetTracking.Service.Services.User.Commands.RegisterUser;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BudgetTracking.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExpensesController : BaseController
    {
        private readonly IMediator _mediator;

        public ExpensesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> CreateExpense([FromBody] CreateExpenseCommand command)
        {
            var result = await _mediator.Send(command);
            return FromResult(result);
        }

        [Authorize]
        [HttpPost("search")]
        public async Task<IActionResult> GetExpensesByUser([FromBody] SearchExpenseCommand command)
        {
            var result = await _mediator.Send(command);
            return FromResult(result);
        }

        [Authorize]
        [HttpGet("from/date")]
        public async Task<IActionResult> GetExpensesByDate([FromQuery] Month month, [FromQuery] int year)
        {
            var result = await _mediator.Send(new GetExpensesByDateQuery { Month = month, Year = year });
            return FromResult(result);
        }
    }
}
