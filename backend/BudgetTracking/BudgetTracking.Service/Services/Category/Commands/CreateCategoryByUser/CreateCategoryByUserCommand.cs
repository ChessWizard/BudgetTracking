using BudgetTracking.Common.Result;
using BudgetTracking.Core.Enums;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BudgetTracking.Service.Services.Category.Commands.CreateCategoryByUser
{
    public class CreateCategoryByUserCommand : IRequest<Result<Unit>>
    {
        public ExpenseType ExpenseType { get; set; }// kategori gelir-gider ayrımı
        public string Title { get; set; }
        public string ImageUrl { get; set; }
    }
}
