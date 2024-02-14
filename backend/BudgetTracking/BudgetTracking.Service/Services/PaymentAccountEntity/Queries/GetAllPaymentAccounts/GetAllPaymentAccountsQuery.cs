using BudgetTracking.Common.Result;
using BudgetTracking.Core.Enums;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BudgetTracking.Service.Services.PaymentAccountEntity.Queries.GetAllPaymentAccounts
{
    public class GetAllPaymentAccountsQuery : IRequest<Result<GetAllPaymentAccountsQueryResult>>
    {
        public PaymentType? PaymentType { get; set; }
    }
}
