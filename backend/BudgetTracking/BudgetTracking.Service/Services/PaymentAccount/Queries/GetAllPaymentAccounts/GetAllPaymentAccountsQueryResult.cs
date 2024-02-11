using BudgetTracking.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BudgetTracking.Service.Services.PaymentAccount.Queries.GetAllPaymentAccounts
{
    public class GetAllPaymentAccountsQueryResult
    {
        public List<PaymentModel> Payments { get; set; }

        public decimal TotalAmount { get; set; }
    }

    public class PaymentModel
    {
        public string Title { get; set; }

        public decimal Amount { get; set; }

        public CurrencyCode CurrencyCode { get; set; }

        public PaymentType PaymentType { get; set; }

        public string Description { get; set; }
    }
}
