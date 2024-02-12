using BudgetTracking.Common.Result;
using BudgetTracking.Core.ContextAccessor;
using BudgetTracking.Core.Repositories;
using BudgetTracking.Data.Extensions.Collection;
using BudgetTracking.Data.Extensions.Linq;
using BudgetTracking.Service.Enums;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace BudgetTracking.Service.Services.ExpenseEntity.Queries.GetExpensesByUser
{
    public class GetExpensesByUserQueryHandler : IRequestHandler<GetExpensesByUserQuery, Result<GetExpensesByUserQueryResult>>
    {
        private readonly ISecurityContextAccessor _securityContextAccessor;
        private readonly UserManager<Core.Entities.User> _userManager;
        private readonly IExpenseRepository _expenseRepository;

        public GetExpensesByUserQueryHandler(ISecurityContextAccessor securityContextAccessor, UserManager<Core.Entities.User> userManager, IExpenseRepository expenseRepository)
        {
            _securityContextAccessor = securityContextAccessor;
            _userManager = userManager;
            _expenseRepository = expenseRepository;
        }

        public async Task<Result<GetExpensesByUserQueryResult>> Handle(GetExpensesByUserQuery request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByIdAsync(_securityContextAccessor.UserId.ToString());

            if (user is null)
                return Result<GetExpensesByUserQueryResult>.Error("Kullanıcı bulunamadı!", (int)HttpStatusCode.NotFound);

            var baseQuery = _expenseRepository.GetAllExpenses();

            // eğer ek olarak dahil edilecek tablolar varsa, ekle
            // IncludeMultiple() extension'ı ile tek seferde çoklu include'lar elde edebiliriz
            if (request.IncludePredicate is not null)
            {
                baseQuery = baseQuery.IncludeMultiple(request.IncludePredicate);
            }

            baseQuery = baseQuery.Include(x => x.Category)
            .Where(x => x.UserId == _securityContextAccessor.UserId);

            // eğer ek koşullar varsa ancak chainle, yoksa tüm hepsini getir
            if (request.WherePredicate is not null)
            {
                baseQuery = baseQuery.Where(request.WherePredicate);
            }

            var expensesByUser = await baseQuery
            .Select(x => new Process
            {
                Category = x.Category.Title,
                ExpenseType = x.ExpenseType,
                CreatedDate = x.ProcessDate,
                ImageUrl = x.Category.ImageUrl,
                Price = x.Price,
                CurrencyCode = x.CurrencyCode
            })
            .ToListAsync();

            if (expensesByUser.IsNullOrNotAny())
                return Result<GetExpensesByUserQueryResult>.Error("Kullanıcıya ait harcama bulunamadı!", (int)HttpStatusCode.NotFound);

            var count = expensesByUser.Count;
            var totalPrice = expensesByUser.Select(x => x.Price)
                .Sum();

            GetExpensesByUserQueryResult result = new()
            {
                Count = count,
                TotalPrice = totalPrice,
                Processes = expensesByUser
            };

            return Result<GetExpensesByUserQueryResult>.Success(result, (int)HttpStatusCode.OK);
        }
    }
}
