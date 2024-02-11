using BudgetTracking.Common.Result;
using BudgetTracking.Core.ContextAccessor;
using BudgetTracking.Core.Entities;
using BudgetTracking.Core.Repositories;
using BudgetTracking.Core.UnitofWork;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace BudgetTracking.Service.Services.Category.Commands.CreateCategoryByUser
{
    public class CreateCategoryByUserCommandHandler : IRequestHandler<CreateCategoryByUserCommand, Result<Unit>>
    {
        private readonly ISecurityContextAccessor _contextAccessor;
        private readonly UserManager<Core.Entities.User> _userManager;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IUnitofWork _unitofWork;

        public CreateCategoryByUserCommandHandler(ISecurityContextAccessor contextAccessor,
            UserManager<Core.Entities.User> userManager,
            ICategoryRepository categoryRepository,
            IUnitofWork unitofWork)
        {
            _contextAccessor = contextAccessor;
            _userManager = userManager;
            _categoryRepository = categoryRepository;
            _unitofWork = unitofWork;
        }

        public async Task<Result<Unit>> Handle(CreateCategoryByUserCommand request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByIdAsync(_contextAccessor.UserId.ToString());

            if (user is null)
                return Result<Unit>.Error("Kullanıcı bulunamadı!", (int)HttpStatusCode.NotFound);

            Core.Entities.Category category = new()
            {
                ExpenseType = request.ExpenseType,
                Title = request.Title,
                ImageUrl = request.ImageUrl,
                User = user
            };

            await _categoryRepository.AddCategoryAsync(category);
            await _unitofWork.SaveChangesAsync();
            return Result<Unit>.Success(Unit.Value, (int)HttpStatusCode.Created);
        }
    }
}
