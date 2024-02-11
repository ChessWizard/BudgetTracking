using BudgetTracking.Common.Entity;
using BudgetTracking.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BudgetTracking.Core.Entities
{
    public class Category : AuditEntity<Guid>
    {
        public string Title { get; set; }

        public string? ImageUrl { get; set; }

        public ExpenseType ExpenseType { get; set; }

        public Guid UserId { get; set; }

        public User User { get; set; }

        // bir kategorinin sahip olduğu birden fazla expense olabilir
        public ICollection<Expense> Expenses { get; set; }
    }
}
