using BudgetTracking.Core.UnitofWork;
using BudgetTracking.Data.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BudgetTracking.Data.UnitofWork
{
    public class UnitofWork : IUnitofWork
    {
        private readonly BudgetTrackingDbContext _context;

        public UnitofWork(BudgetTrackingDbContext context)
        {
            _context = context;
        }

        public async Task<int> SaveChangesAsync()
         => await _context.SaveChangesAsync();
    }
}
