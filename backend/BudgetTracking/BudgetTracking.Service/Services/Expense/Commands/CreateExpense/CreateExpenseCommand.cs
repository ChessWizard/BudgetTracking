using BudgetTracking.Common.Result;
using BudgetTracking.Core.Enums;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BudgetTracking.Service.Services.Expense.Commands.CreateExpense
{
    public class CreateExpenseCommand : IRequest<Result<Unit>>
    {
        public Guid CategoryId { get; set; }

        public decimal Price { get; set; }

        public ExpenseType ExpenseType { get; set; }

        public string? Description { get; set; }
    }
}
