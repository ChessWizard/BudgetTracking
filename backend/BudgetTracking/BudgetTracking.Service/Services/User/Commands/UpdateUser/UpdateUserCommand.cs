using BudgetTracking.Common.Result;
using BudgetTracking.Core.Enums;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BudgetTracking.Service.Services.User.Commands.UpdateUser
{
    public class UpdateUserCommand : IRequest<Result<Unit>>
    {
        public string Name { get; set; }

        public string Surname { get; set; }

        public DateOnly? BirthDate { get; set; }

        public GenderType? GenderType { get; set; }
    }
}
