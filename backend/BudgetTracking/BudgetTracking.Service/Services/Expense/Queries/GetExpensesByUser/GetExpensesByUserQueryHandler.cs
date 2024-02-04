using BudgetTracking.Common.Result;
using BudgetTracking.Core.ContextAccessor;
using BudgetTracking.Core.Repositories;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace BudgetTracking.Service.Services.Expense.Queries.GetExpensesByUser
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

            var expensesByUser = await _expenseRepository.GetAllExpenses()
                .Include(x => x.Category)
                .Where(x => x.UserId == user.Id)
                .Select(x => new Process
                {
                    Category = x.Category.Title,
                    ExpenseType = x.ExpenseType,
                    CreatedDate = DateOnly.FromDateTime(x.CreatedOn.DateTime),
                    ImageUrl = x.Category.ImageUrl,
                    Price = x.Price,
                })
                .ToListAsync();

            if(expensesByUser is null || !expensesByUser.Any())
                return Result<GetExpensesByUserQueryResult>.Error("Kullanıcıya ait harcama bulunamadı!", (int)HttpStatusCode.NotFound);

            var count = await _expenseRepository.GetAllExpenses()
                .CountAsync();

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
