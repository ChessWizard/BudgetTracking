using BudgetTracking.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BudgetTracking.Core.Repositories
{
    public interface IUserRepository
    {
        Task AddUserAsync(User user);

        Task UpdateUserAsync(User user);

        IQueryable<User> GetAll();
    }
}
