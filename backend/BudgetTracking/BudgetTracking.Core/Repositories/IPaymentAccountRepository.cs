using BudgetTracking.Core.Entities;
using BudgetTracking.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BudgetTracking.Core.Repositories
{
    public interface IPaymentAccountRepository
    {
        Task AddPaymentAccountAsync(PaymentAccount paymentAccount);

        IQueryable<PaymentAccount> GetAllPaymentAccounts();

        IQueryable<PaymentAccount> GetAllPaymentAccountsByUser(Guid userId);

        Task<PaymentAccount> GetPaymentAccountAsync(Expression<Func<PaymentAccount, bool>> predicate);

        Task UpdatePaymentAccountAsync(PaymentAccount paymentAccount);

        Task UpdatePaymentAccountAmountByExpenseTypeAsync(PaymentAccount paymentAccount, ExpenseType expenseType, decimal price);
    }
}
