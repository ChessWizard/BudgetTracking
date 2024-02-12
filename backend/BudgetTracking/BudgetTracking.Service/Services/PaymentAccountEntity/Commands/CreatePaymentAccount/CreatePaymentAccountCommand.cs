using BudgetTracking.Common.Result;
using BudgetTracking.Core.Enums;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BudgetTracking.Service.Services.PaymentAccountEntity.Commands.CreatePaymentAccount
{
    public class CreatePaymentAccountCommand : IRequest<Result<Unit>>
    {
        public string Title { get; set; }

        public decimal InitialAmount { get; set; }// Halihazırda var olan miktar

        public CurrencyCode CurrencyCode { get; set; }// para birimi

        public PaymentType PaymentType { get; set; }

        public string Description { get; set; }
    }
}
