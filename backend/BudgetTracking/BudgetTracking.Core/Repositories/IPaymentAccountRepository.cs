using BudgetTracking.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BudgetTracking.Core.Repositories
{
    public interface IPaymentAccountRepository
    {
        Task AddPaymentAccountAsync(PaymentAccount paymentAccount);

        IQueryable<PaymentAccount> GetAllPaymentAccounts();

        IQueryable<PaymentAccount> GetAllPaymentAccountsByUser(Guid userId);
    }
}
