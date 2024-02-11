using BudgetTracking.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BudgetTracking.Service.Services.ExpenseEntity.Queries.GetExpensesByUser
{
    public class GetExpensesByUserQueryResult
    {
        public List<Process> Processes { get; set; }
        public int Count { get; set; }
        public decimal TotalPrice { get; set; }
    }

    public class Process
    {
        public ExpenseType ExpenseType { get; set; }
        public decimal Price { get; set; }
        public string ImageUrl { get; set; }
        public string Category { get; set; }
        public DateOnly CreatedDate { get; set; }
        public CurrencyCode CurrencyCode { get; set; }
    }
}
