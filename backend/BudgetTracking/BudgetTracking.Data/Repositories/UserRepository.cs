using BudgetTracking.Core.Entities;
using BudgetTracking.Core.Repositories;
using BudgetTracking.Core.UnitofWork;
using BudgetTracking.Data.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BudgetTracking.Data.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly BudgetTrackingDbContext _context;

        public UserRepository(BudgetTrackingDbContext context)
        {
            _context = context;
        }

        public async Task AddUserAsync(User user)
        {
            await _context.Users.AddAsync(user);
        }
    }
}
