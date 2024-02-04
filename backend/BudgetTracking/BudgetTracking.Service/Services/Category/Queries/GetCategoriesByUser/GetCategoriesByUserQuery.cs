using BudgetTracking.Common.Result;
using BudgetTracking.Core.Enums;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BudgetTracking.Service.Services.Category.Queries.GetCategoriesByUser
{
    public class GetCategoriesByUserQuery : IRequest<Result<GetCategoriesByUserQueryResult>>
    {
        public ExpenseType ExpenseType { get; set; }
    }
}
