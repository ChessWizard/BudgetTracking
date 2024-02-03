using BudgetTracking.Common.Entity.Interfaces;
using BudgetTracking.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BudgetTracking.Core.Repositories
{
    public interface IUserRefreshTokenRepository
    {
        Task<UserRefreshToken> GetRefreshTokenAsync(Expression<Func<UserRefreshToken, bool>> predicate);

        Task AddRefreshTokenAsync(UserRefreshToken userRefreshToken);

        Task UpdateRefreshTokenAsync(UserRefreshToken userRefreshToken);

        Task RemoveRefreshTokenAsync(UserRefreshToken userRefreshToken);
    }
}
