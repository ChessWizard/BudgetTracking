using BudgetTracking.Common.Controller;
using BudgetTracking.Service.Services.Authentication.Commands.RevokeRefreshToken;
using BudgetTracking.Service.Services.Authentication.Commands.UserLogin;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BudgetTracking.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthsController : BaseController
    {
        private readonly IMediator _mediator;

        public AuthsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("login")]
        public async Task<IActionResult> LoginUser([FromBody] UserLoginCommand command)
        {
            var result = await _mediator.Send(command);
            return FromResult(result);
        }

        [HttpDelete("logout")]
        public async Task<IActionResult> LogoutUser([FromQuery] RevokeRefreshTokenCommand command)
        {
            var result = await _mediator.Send(command);
            return FromResult(result);
        }
    }
}
