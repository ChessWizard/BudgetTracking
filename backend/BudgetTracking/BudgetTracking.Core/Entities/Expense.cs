using BudgetTracking.Common.Entity;
using BudgetTracking.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BudgetTracking.Core.Entities
{
    public class Expense : AuditEntity<Guid>
    {
        public Guid UserId { get; set; }

        public User User { get; set; }

        public ExpenseType ExpenseType { get; set; }

        public decimal Price { get; set; }

        public CurrencyCode CurrencyCode { get; set; }

        public string Description { get; set; }

        public DateOnly ProcessDate { get; set; }

        public TimeOnly ProcessTime { get; set; }

        // bir expense yalnızca 1 kategoriye aittir
        public Guid CategoryId { get; set; }

        public Category Category { get; set; }

        // 1 expense yalnızca 1 PaymentAccount'a aittir
        public Guid? PaymentAccountId { get; set; }

        public PaymentAccount PaymentAccount { get; set; }
    }
}
