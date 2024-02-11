using BudgetTracking.Common.Result;
using BudgetTracking.Core.Enums;
using BudgetTracking.Service.Services.ExpenseEntity.Queries.GetExpensesByUser;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BudgetTracking.Service.Services.ExpenseEntity.Commands.SearchExpense
{
    public class SearchExpenseCommand : IRequest<Result<GetExpensesByUserQueryResult>>
    {
        // Hiçbir alanın doldurulması zorunlu değildir
        // Yalnızca doldurulan alanlara uygun bir biçimde search işlemi gerçekleştirilir
        public List<Guid>? CategoryIds { get; set; }

        public DateOnly? ProcessDateStart { get; set; }

        public DateOnly? ProcessDateEnd { get; set; }

        public TimeOnly? ProcessTimeStart { get; set; }

        public TimeOnly? ProcessTimeEnd { get; set; }

        public CurrencyCode? CurrencyCode { get; set; }

        public string? MinPrice { get; set; }

        public string? MaxPrice { get; set; }
    }
}
