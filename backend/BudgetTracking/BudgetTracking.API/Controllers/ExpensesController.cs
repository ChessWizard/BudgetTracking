using BudgetTracking.Common.Controller;
using BudgetTracking.Service.Services.Expense.Commands.CreateExpense;
using BudgetTracking.Service.Services.Expense.Queries.GetExpensesByUser;
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
        [HttpGet]
        public async Task<IActionResult> GetExpensesByUser()
        {
            var result = await _mediator.Send(new GetExpensesByUserQuery());
            return FromResult(result);
        }
    }
}
