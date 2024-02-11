using BudgetTracking.Common.Result;
using BudgetTracking.Core.Entities;
using BudgetTracking.Data.Extensions.Collection;
using BudgetTracking.Service.Services.ExpenseEntity.Queries.GetExpensesByDate;
using BudgetTracking.Service.Services.ExpenseEntity.Queries.GetExpensesByUser;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BudgetTracking.Service.Services.ExpenseEntity.Commands.SearchExpense
{
    public class SearchExpenseCommandHandler : IRequestHandler<SearchExpenseCommand, Result<GetExpensesByUserQueryResult>>
    {
        private readonly IMediator _mediator;

        public SearchExpenseCommandHandler(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task<Result<GetExpensesByUserQueryResult>> Handle(SearchExpenseCommand request, CancellationToken cancellationToken)
        {
            var wherePredicate = GetExpenseBySearchWherePredicate(request);

            var result = await _mediator.Send(new GetExpensesByUserQuery { WherePredicate = wherePredicate });
            return result;
        }

        private static Expression<Func<Expense, bool>> GetExpenseBySearchWherePredicate(SearchExpenseCommand request)
        {
            ParameterExpression parameter = Expression.Parameter(typeof(Expense), "expense");
            Expression predicateBody = Expression.Constant(true); // Başlangıçta her şeyi doğru kabul ediyoruz

            // Kategori filtresi
            if (!request.CategoryIds.IsNullOrNotAny())
            {
                Expression categoryIdFilter = Expression.Call(
                    typeof(List<Guid>),
                    nameof(List<Guid>.Contains),
                    Type.EmptyTypes,
                    Expression.Constant(request.CategoryIds),
                    Expression.Property(parameter, nameof(Expense.CategoryId))
                );

                predicateBody = Expression.AndAlso(predicateBody, categoryIdFilter);
            }

            // Tarih aralığı filtresi
            if (request.ProcessDateStart.HasValue)
            {
                Expression dateStartFilter = Expression.GreaterThanOrEqual(
                    Expression.Property(
                        Expression.Property(parameter, nameof(Expense.ProcessDate)),
                        nameof(DateTime.Date)
                    ),
                    Expression.Constant(request.ProcessDateStart.Value)
                );

                predicateBody = Expression.AndAlso(predicateBody, dateStartFilter);
            }

            if (request.ProcessDateEnd.HasValue)
            {
                Expression dateEndFilter = Expression.LessThanOrEqual(
                    Expression.Property(
                        Expression.Property(parameter, nameof(Expense.ProcessDate)),
                        nameof(DateTime.Date)
                    ),
                    Expression.Constant(request.ProcessDateEnd.Value)
                );

                predicateBody = Expression.AndAlso(predicateBody, dateEndFilter);
            }

            // Saat aralığı filtresi
            if (request.ProcessTimeStart.HasValue)
            {
                Expression timeStartFilter = Expression.GreaterThanOrEqual(
                    Expression.Property(
                        Expression.Property(parameter, nameof(Expense.ProcessTime)),
                        nameof(DateTime.TimeOfDay)
                    ),
                    Expression.Constant(request.ProcessTimeStart.Value.ToTimeSpan())
                );

                predicateBody = Expression.AndAlso(predicateBody, timeStartFilter);
            }

            if (request.ProcessTimeEnd.HasValue)
            {
                Expression timeEndFilter = Expression.LessThanOrEqual(
                    Expression.Property(
                        Expression.Property(parameter, nameof(Expense.ProcessDate)),
                        nameof(DateTime.TimeOfDay)
                    ),
                    Expression.Constant(request.ProcessTimeEnd.Value.ToTimeSpan())
                );

                predicateBody = Expression.AndAlso(predicateBody, timeEndFilter);
            }

            // Para birimi filtresi
            if (request.CurrencyCode.HasValue)
            {
                Expression currencyCodeFilter = Expression.Equal(
                    Expression.Property(parameter, nameof(Expense.CurrencyCode)),
                    Expression.Constant(request.CurrencyCode.Value)
                );

                predicateBody = Expression.AndAlso(predicateBody, currencyCodeFilter);
            }

            // MinPrice ve MaxPrice filtreleri
            if (!string.IsNullOrEmpty(request.MinPrice))
            {
                Expression minPriceFilter = Expression.GreaterThanOrEqual(
                    Expression.Property(
                        Expression.Constant(decimal.Parse(request.MinPrice)),
                        nameof(decimal.Parse)
                    ),
                    Expression.Property(parameter, nameof(Expense.Price))
                );

                predicateBody = Expression.AndAlso(predicateBody, minPriceFilter);
            }

            if (!string.IsNullOrEmpty(request.MaxPrice))
            {
                Expression maxPriceFilter = Expression.LessThanOrEqual(
                    Expression.Property(
                        Expression.Constant(decimal.Parse(request.MaxPrice)),
                        nameof(decimal.Parse)
                    ),
                    Expression.Property(parameter, nameof(Expense.Price))
                );

                predicateBody = Expression.AndAlso(predicateBody, maxPriceFilter);
            }

            return Expression.Lambda<Func<Expense, bool>>(predicateBody, parameter);

        }
    }
}
