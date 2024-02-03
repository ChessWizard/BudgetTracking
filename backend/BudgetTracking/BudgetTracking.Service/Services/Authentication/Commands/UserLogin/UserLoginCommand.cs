using BudgetTracking.Common.Result;
using BudgetTracking.Data.Dto;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BudgetTracking.Service.Services.Authentication.Commands.UserLogin
{
    public class UserLoginCommand : IRequest<Result<TokenDto>>
    {
        public string Email { get; set; }

        public string Password { get; set; }
    }
}
