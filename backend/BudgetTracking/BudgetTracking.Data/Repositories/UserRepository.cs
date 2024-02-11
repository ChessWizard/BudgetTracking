using BudgetTracking.Core.Entities;
using BudgetTracking.Core.Repositories;
using BudgetTracking.Core.UnitofWork;
using BudgetTracking.Data.Context;
using Microsoft.EntityFrameworkCore;
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

        public IQueryable<User> GetAll()
         => _context.Users.AsNoTracking();

        public async Task UpdateUserAsync(User user)
         => await Task.FromResult(_context.Users.Update(user));
    }
}
