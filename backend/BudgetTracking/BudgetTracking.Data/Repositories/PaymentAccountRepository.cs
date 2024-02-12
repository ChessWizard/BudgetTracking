using BudgetTracking.Core.Entities;
using BudgetTracking.Core.Enums;
using BudgetTracking.Core.Repositories;
using BudgetTracking.Data.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BudgetTracking.Data.Repositories
{
    public class PaymentAccountRepository : IPaymentAccountRepository
    {
        private readonly BudgetTrackingDbContext _context;

        public PaymentAccountRepository(BudgetTrackingDbContext context)
        {
            _context = context;
        }

        public async Task AddPaymentAccountAsync(PaymentAccount paymentAccount)
         => await _context.PaymentAccounts.AddAsync(paymentAccount);


        public IQueryable<PaymentAccount> GetAllPaymentAccounts()
         => _context.PaymentAccounts
            .AsNoTracking();

        public IQueryable<PaymentAccount> GetAllPaymentAccountsByUser(Guid userId)
         => GetAllPaymentAccounts().Where(x => x.UserId == userId);

        public async Task<PaymentAccount> GetPaymentAccountAsync(Expression<Func<PaymentAccount, bool>> predicate)
         => await _context.PaymentAccounts
            .FirstOrDefaultAsync(predicate);

        public async Task UpdatePaymentAccountAmountByExpenseTypeAsync(PaymentAccount paymentAccount, ExpenseType expenseType, decimal price)
        {
            switch (expenseType)
            {
                case ExpenseType.Revenue:
                    paymentAccount.Amount += price;
                    break;
                case ExpenseType.Outgoing:
                    paymentAccount.Amount -= price;
                    break;
            }

            await UpdatePaymentAccountAsync(paymentAccount);
        }


        public async Task UpdatePaymentAccountAsync(PaymentAccount paymentAccount)
        => await Task.FromResult(_context.PaymentAccounts.Update(paymentAccount));
    }
}
