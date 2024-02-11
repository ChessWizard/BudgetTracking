using BudgetTracking.Common.Result;
using BudgetTracking.Service.Enums;
using BudgetTracking.Service.Services.ExpenseEntity.Queries.GetExpensesByUser;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BudgetTracking.Service.Services.ExpenseEntity.Queries.GetExpensesByDate
{
    public class GetExpensesByDateQuery : IRequest<Result<GetExpensesByUserQueryResult>>
    {
        // Ay ve yıl değerlerine göre filtrelenmiş işlemler getirilecek
        public Month Month { get; set; }

        public int Year { get; set; }
    }
}
