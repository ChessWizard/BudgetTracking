using BudgetTracking.Common.Result;
using BudgetTracking.Core.ContextAccessor;
using BudgetTracking.Core.Repositories;
using MediatR;
using System.Net;
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
        private readonly IPaymentAccountRepository _paymentAccountRepository;

        public CreateExpenseCommandHandler(IExpenseRepository expenseRepository, ICategoryRepository categoryRepository, ISecurityContextAccessor securityContextAccessor, UserManager<Core.Entities.User> userManager, IUnitofWork unitofWork, IPaymentAccountRepository paymentAccountRepository)
        {
            _expenseRepository = expenseRepository;
            _categoryRepository = categoryRepository;
            _securityContextAccessor = securityContextAccessor;
            _userManager = userManager;
            _unitofWork = unitofWork;
            _paymentAccountRepository = paymentAccountRepository;
        }

        public async Task<Result<Unit>> Handle(CreateExpenseCommand request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByIdAsync(_securityContextAccessor.UserId.ToString());
            if(user is null)
                return Result<Unit>.Error("Kullanıcı bulunamadı!", (int)HttpStatusCode.NotFound);

            var category = await _categoryRepository.GetCategoryAsync(x => x.Id == request.CategoryId && x.UserId == user.Id);
            if (category is null || category.ExpenseType != request.ExpenseType)
                return Result<Unit>.Error("Kullanıcıya ait uygun bir kategori bulunamadı!", (int)HttpStatusCode.NotFound);

            var paymentAccount = await _paymentAccountRepository.GetPaymentAccountAsync(x => x.Id == request.PaymentAccountId 
                                                                                        && x.UserId == _securityContextAccessor.UserId);
            if (paymentAccount is null)
                return Result<Unit>.Error("Kullanıcıya ait uygun bir hesap bulunamadı!", (int)HttpStatusCode.NotFound);

            Expense expense = new()
            {
                Category = category,
                Description = request.Description ?? "",
                ExpenseType = request.ExpenseType,
                Price = request.ExpenseType == ExpenseType.Outgoing ? decimal.Parse($"-{request.Price}") : request.Price,
                ProcessDate = request.ProcessDate,
                ProcessTime = request.ProcessTime,
                User = user,
                PaymentAccount = paymentAccount,
            };

            await _expenseRepository.AddExpenseAsync(expense);

            // Expense eklendikten sonra gelir veya gider durumuna göre seçili hesaptaki para değeri değişsin
            await _paymentAccountRepository.UpdatePaymentAccountAmountByExpenseTypeAsync(paymentAccount, request.ExpenseType, request.Price);

            await _unitofWork.SaveChangesAsync();
            return Result<Unit>.Success("Harcama ekleme işlemi başarılı!", (int)HttpStatusCode.Created);
        }
    }
}
