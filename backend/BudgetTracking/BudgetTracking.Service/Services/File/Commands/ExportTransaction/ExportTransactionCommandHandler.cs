using BudgetTracking.Core.Entities;
using BudgetTracking.Core.Enums;
using BudgetTracking.Data.Dto;
using BudgetTracking.Data.Extensions.Linq;
using BudgetTracking.Service.Services.ExpenseEntity.Queries.GetExpensesByUser;
using BudgetTracking.Service.Services.File.Commands.ExportFile;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BudgetTracking.Service.Services.File.Commands.ExportTransaction
{
    public class ExportTransactionCommandHandler : IRequestHandler<ExportTransactionCommand, (byte[], string, string)>
    {
        private readonly IMediator _mediator;

        public ExportTransactionCommandHandler(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task<(byte[], string, string)> Handle(ExportTransactionCommand request, CancellationToken cancellationToken)
        {
            var userTransactions = await _mediator.Send(new GetExpensesByUserQuery() 
            { 
                IncludePredicate = new List<string> { nameof(PaymentAccount) },
                WherePredicate = GetTransactionWherePredicate(request)
            }
            , cancellationToken);
            var mappedForExcel = userTransactions.Data.Processes.Select(x => new ExportTransactionDto
            {
                Category = x.Category,
                CreatedDate = x.CreatedDate,
                Currency = x.CurrencyCode.ToString(),
                Expense = x.ExpenseType is ExpenseType.Revenue ? "Gelir" : "Gider",
                Price = x.Price,
            });

            var result = await _mediator.Send(new ExportFileCommand 
            { 
                ExportFileType = request.ExportFileType, 
                ExportableData = mappedForExcel, 
                ExportableDataClassName = $"BudgetTracking.Data.Dto.{nameof(ExportTransactionDto)}"
            });
            return result;
        }

        private static Expression<Func<Expense, bool>> GetTransactionWherePredicate(ExportTransactionCommand request)
        {
            Expression<Func<Expense, bool>> predicate = expense => true;

            if (request.PaymentAccountId.HasValue)
            {
                predicate = predicate.And(expense => expense.PaymentAccountId == request.PaymentAccountId.Value);
            }

            if (request.ProcessStartDate.HasValue && request.ProcessEndDate.HasValue)
            {
                predicate = predicate.And(expense => expense.ProcessDate >= request.ProcessStartDate.Value && expense.ProcessDate <= request.ProcessEndDate.Value);
            }
            else if (request.ProcessStartDate.HasValue)
            {
                predicate = predicate.And(expense => expense.ProcessDate >= request.ProcessStartDate.Value);
            }
            else if (request.ProcessEndDate.HasValue)
            {
                predicate = predicate.And(expense => expense.ProcessDate <= request.ProcessEndDate.Value);
            }

            return predicate;
        }
    }
}
