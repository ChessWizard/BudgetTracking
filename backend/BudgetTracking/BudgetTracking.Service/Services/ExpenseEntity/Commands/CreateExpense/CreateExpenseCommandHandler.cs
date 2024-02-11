using BudgetTracking.Common.Result;
using BudgetTracking.Core.ContextAccessor;
using BudgetTracking.Core.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using BudgetTracking.Core.Entities;
using Microsoft.AspNetCore.Identity;
using BudgetTracking.Core.UnitofWork;
using BudgetTracking.Core.Enums;

namespace BudgetTracking.Service.Services.ExpenseEntity.Commands.CreateExpense
{
    public class CreateExpenseCommandHandler : IRequestHandler<CreateExpenseCommand, Result<Unit>>
    {
        private readonly IExpenseRepository _expenseRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly ISecurityContextAccessor _securityContextAccessor;
        private readonly UserManager<Core.Entities.User> _userManager;
        private readonly IUnitofWork _unitofWork;

        public CreateExpenseCommandHandler(IExpenseRepository expenseRepository, ICategoryRepository categoryRepository, ISecurityContextAccessor securityContextAccessor, UserManager<Core.Entities.User> userManager, IUnitofWork unitofWork)
        {
            _expenseRepository = expenseRepository;
            _categoryRepository = categoryRepository;
            _securityContextAccessor = securityContextAccessor;
            _userManager = userManager;
            _unitofWork = unitofWork;
        }

        public async Task<Result<Unit>> Handle(CreateExpenseCommand request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByIdAsync(_securityContextAccessor.UserId.ToString());
            if(user is null)
                return Result<Unit>.Error("Kullanıcı bulunamadı!", (int)HttpStatusCode.NotFound);

            var category = await _categoryRepository.GetCategoryAsync(x => x.Id == request.CategoryId && x.UserId == user.Id);
            if (category is null)
                return Result<Unit>.Error("Kullanıcıya ait uygun bir kategori bulunamadı!", (int)HttpStatusCode.NotFound);

            Core.Entities.Expense expense = new()
            {
                Category = category,
                Description = request.Description ?? "",
                ExpenseType = request.ExpenseType,
                Price = request.ExpenseType == ExpenseType.Outgoing ? decimal.Parse($"-{request.Price}") : request.Price,
                ProcessDate = request.ProcessDate,
                ProcessTime = request.ProcessTime,
                User = user,
            };

            await _expenseRepository.AddExpenseAsync(expense);
            await _unitofWork.SaveChangesAsync();
            return Result<Unit>.Success("Harcama ekleme işlemi başarılı!", (int)HttpStatusCode.Created);
        }
    }
}
