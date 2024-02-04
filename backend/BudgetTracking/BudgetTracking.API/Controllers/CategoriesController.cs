using BudgetTracking.Common.Controller;
using BudgetTracking.Core.Enums;
using BudgetTracking.Service.Services.Category.Queries.GetCategoriesByUser;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BudgetTracking.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : BaseController
    {
        private readonly IMediator _mediator;

        public CategoriesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetCategoriesByUser([FromQuery] ExpenseType expenseType)
        {
            var result = await _mediator.Send(new GetCategoriesByUserQuery { ExpenseType = expenseType });
            return FromResult(result);
        }
    }
}
