using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BudgetTracking.Service.Services.Category.Queries.GetCategoriesByUser
{
    public class GetCategoriesByUserQueryResult
    {
        public List<CategoryModel> Categories { get; set; }
    }

    public class CategoryModel
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string ImageUrl { get; set; }
    }
}
