using BudgetTracking.Common.Result;
using BudgetTracking.Core.Entities;
using BudgetTracking.Service.Services.ExpenseEntity.Queries.GetExpensesByUser;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BudgetTracking.Service.Services.ExpenseEntity.Queries.GetExpensesByDate
{
    public class GetExpensesByDateQueryHandler : IRequestHandler<GetExpensesByDateQuery, Result<GetExpensesByUserQueryResult>>
    {
        private readonly IMediator _mediator;

        public GetExpensesByDateQueryHandler(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task<Result<GetExpensesByUserQueryResult>> Handle(GetExpensesByDateQuery request, CancellationToken cancellationToken)
        {
            var wherePredicate = GetExpenseByDateWherePredicate(request);

            var result = await _mediator.Send(new GetExpensesByUserQuery { WherePredicate = wherePredicate });
            return result;
        }

        private static Expression<Func<Expense, bool>> GetExpenseByDateWherePredicate(GetExpensesByDateQuery request)
        {
            // custom biçimde lambda expression tanımı
            
            // Expense entity'sini gezmemize yarayacak "expense" isminde bir lambda parametresi yaratalım
            ParameterExpression parameter = Expression.Parameter(typeof(Expense), "expense");

            // Ardından aşağıdaki koşulumuzu oluşturalım
            //  (expense => expense.ProcessDate.Month == (int)request.Month && expense.ProcessDate.Year == request.Year)
            Expression<Func<Expense, bool>> expenseByDateWherePredicate = Expression.Lambda<Func<Expense, bool>>(
                Expression.AndAlso(
                    Expression.Equal(
                        Expression.Property(
                            Expression.Property(parameter, nameof(Expense.ProcessDate)),
                            nameof(DateTime.Month)
                            ),
                        Expression.Constant((int)request.Month)
                    ),
                    Expression.Equal(
                        Expression.Property(
                            Expression.Property(parameter, nameof(Expense.ProcessDate)),
                            nameof(DateTime.Year)
                            ),
                        Expression.Constant(request.Year)
                        )
                    ),
                parameter
           );

            return expenseByDateWherePredicate;
        }
    }
}
