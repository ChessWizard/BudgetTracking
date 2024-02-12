using BudgetTracking.Common.Controller;
using BudgetTracking.Service.Services.PaymentAccountEntity.Commands.CreatePaymentAccount;
using BudgetTracking.Service.Services.PaymentAccountEntity.Queries.GetAllPaymentAccounts;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BudgetTracking.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentsController : BaseController
    {
        private readonly IMediator _mediator;

        public PaymentsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetAllPaymentAccounts()
        {
            var result = await _mediator.Send(new GetAllPaymentAccountsQuery());
            return FromResult(result);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> CreatePaymentAccount([FromBody] CreatePaymentAccountCommand command)
        {
            var result = await _mediator.Send(command);
            return FromResult(result);
        }
    }
}
