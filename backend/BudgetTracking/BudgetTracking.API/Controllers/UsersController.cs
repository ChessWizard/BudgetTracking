using BudgetTracking.Common.Controller;
using BudgetTracking.Service.Services.Authentication.Commands.UserLogin;
using BudgetTracking.Service.Services.User.Commands.RegisterUser;
using BudgetTracking.Service.Services.User.Commands.UpdateUser;
using BudgetTracking.Service.Services.User.Queries.GetUserDetails;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BudgetTracking.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : BaseController
    {
        private readonly IMediator _mediator;

        public UsersController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("register")]
        public async Task<IActionResult> RegisterUser([FromBody] RegisterUserCommand command)
        {
            var result = await _mediator.Send(command);
            return FromResult(result);
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetUserDetails()
        {
            var result = await _mediator.Send(new GetUserDetailsQuery());
            return FromResult(result);
        }

        [HttpPatch]
        [Authorize]
        public async Task<IActionResult> UpdateUserDetails([FromBody] UpdateUserCommand command)
        {
            var result = await _mediator.Send(command);
            return FromResult(result);
        }
    }
}
