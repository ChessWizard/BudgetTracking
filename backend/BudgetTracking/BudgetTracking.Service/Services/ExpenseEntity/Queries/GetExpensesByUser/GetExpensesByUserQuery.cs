using BudgetTracking.Common.Result;
using BudgetTracking.Core.Entities;
using BudgetTracking.Service.Enums;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BudgetTracking.Service.Services.ExpenseEntity.Queries.GetExpensesByUser
{
    public class GetExpensesByUserQuery : IRequest<Result<GetExpensesByUserQueryResult>>
    {
        // Gelen farklı istekler için "generic where clause" yaratarak genel bir expense response yapısı yaratıyoruz
        // bu sayede kod tekrarının önüne geçilmiş olur
        public Expression<Func<Expense, bool>>? WherePredicate { get; set; }

        // generic include
        public List<string> IncludePredicate { get; set; }
    }
}
