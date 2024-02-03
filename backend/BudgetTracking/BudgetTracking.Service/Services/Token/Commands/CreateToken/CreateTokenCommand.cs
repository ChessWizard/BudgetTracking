using BudgetTracking.Core.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BudgetTracking.Service.Services.Token.Commands.CreateToken
{
    public class CreateTokenCommand : IRequest<Data.Dto.TokenDto>
    {
        public Core.Entities.User User { get; set; }
    }
}
