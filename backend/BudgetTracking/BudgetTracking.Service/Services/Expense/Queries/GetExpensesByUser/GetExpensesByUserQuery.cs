using BudgetTracking.Common.Result;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BudgetTracking.Service.Services.Expense.Queries.GetExpensesByUser
{
    public class GetExpensesByUserQuery : IRequest<Result<GetExpensesByUserQueryResult>>
    {
    }
}
