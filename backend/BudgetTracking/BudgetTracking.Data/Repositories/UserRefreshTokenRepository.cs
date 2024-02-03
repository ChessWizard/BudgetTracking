using BudgetTracking.Core.Entities;
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
    public class UserRefreshTokenRepository : IUserRefreshTokenRepository
    {
        private readonly BudgetTrackingDbContext _context;

        public UserRefreshTokenRepository(BudgetTrackingDbContext context)
        {
            _context = context;
        }

        public async Task AddRefreshTokenAsync(UserRefreshToken userRefreshToken)
        {
            await _context.UserRefreshTokens.AddAsync(userRefreshToken);
        }

        public async Task<UserRefreshToken> GetRefreshTokenAsync(Expression<Func<UserRefreshToken, bool>> predicate)
         => await _context.UserRefreshTokens.FirstOrDefaultAsync(predicate);

        public async Task RemoveRefreshTokenAsync(UserRefreshToken userRefreshToken)
        {
            await Task.FromResult(_context.UserRefreshTokens.Remove(userRefreshToken));
        }

        public async Task UpdateRefreshTokenAsync(UserRefreshToken userRefreshToken)
        {
            await Task.FromResult(_context.UserRefreshTokens.Update(userRefreshToken));
        }
    }
}
