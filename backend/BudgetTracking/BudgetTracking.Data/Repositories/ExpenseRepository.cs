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
    public class ExpenseRepository : IExpenseRepository
    {
        private readonly BudgetTrackingDbContext _context;

        public ExpenseRepository(BudgetTrackingDbContext context)
        {
            _context = context;
        }

        public async Task AddExpenseAsync(Expense expense)
        {
            await _context.Expenses.AddAsync(expense);
        }

        public IQueryable<Expense> GetAllExpenses()
         => _context.Expenses
            .AsNoTracking();
    }
}
