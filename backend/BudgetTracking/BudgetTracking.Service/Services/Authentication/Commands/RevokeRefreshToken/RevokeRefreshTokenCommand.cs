using BudgetTracking.Common.Result;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BudgetTracking.Service.Services.Authentication.Commands.RevokeRefreshToken
{
    public class RevokeRefreshTokenCommand : IRequest<Result<Unit>>
    {
        public string RefreshToken { get; set; }
    }
}
