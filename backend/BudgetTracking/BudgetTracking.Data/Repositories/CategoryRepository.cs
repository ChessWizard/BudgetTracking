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
    public class CategoryRepository : ICategoryRepository
    {
        private readonly BudgetTrackingDbContext _context;

        public CategoryRepository(BudgetTrackingDbContext context)
        {
            _context = context;
        }

        public async Task AddCategoryAsync(Category category)
        {
            await _context.Categories.AddAsync(category);
        }

        public IQueryable<Category> GetAllCategoriesByUser(Guid userId)
         => _context.Categories
            .Where(x => x.UserId == userId);

        public async Task<Category> GetCategoryAsync(Expression<Func<Category, bool>> predicate)
         => await _context.Categories.FirstOrDefaultAsync(predicate);
    }
}
