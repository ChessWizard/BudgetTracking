using BudgetTracking.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BudgetTracking.Core.Repositories
{
    public interface ICategoryRepository
    {
        Task<Category> GetCategoryAsync(Expression<Func<Category, bool>> predicate);

        IQueryable<Category> GetAllCategoriesByUser(Guid userId);

        Task AddCategoryAsync(Category category);
    }
}
