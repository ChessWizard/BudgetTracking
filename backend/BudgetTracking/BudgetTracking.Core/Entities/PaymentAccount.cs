using BudgetTracking.Common.Entity;
using BudgetTracking.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BudgetTracking.Core.Entities
{
    public class PaymentAccount : AuditEntity<Guid>
    {
        public string Title { get; set; }

        public decimal Amount { get; set; }

        public string Description { get; set; }

        public CurrencyCode CurrencyCode { get; set; }

        public PaymentType PaymentType { get; set; }

        // bir payment account yalnızca 1 kullanıcıya ait olabilir
        public Guid UserId { get; set; }

        public User User { get; set; }

        // bir payment account'ın bağlı olduğu birden fazla expense olabilir
        public ICollection<Expense> Expenses { get; set; }
    }
}
