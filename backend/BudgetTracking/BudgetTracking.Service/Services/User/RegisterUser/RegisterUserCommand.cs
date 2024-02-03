using BudgetTracking.Common.Result;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BudgetTracking.Service.Services.User.RegisterUser
{
    public class RegisterUserCommand : IRequest<Result<Unit>>
    {
        public string Email { get; set; }

        public string Password { get; set; }

        public string RetryPassword { get; set; }
    }
}
