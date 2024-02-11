using BudgetTracking.Core.Entities;
using BudgetTracking.Core.Repositories;
using BudgetTracking.Data.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
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
    }
}
