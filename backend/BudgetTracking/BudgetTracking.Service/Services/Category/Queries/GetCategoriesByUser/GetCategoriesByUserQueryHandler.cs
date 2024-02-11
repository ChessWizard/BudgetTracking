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

namespace BudgetTracking.Service.Services.Category.Queries.GetCategoriesByUser
{
    public class GetCategoriesByUserQueryHandler : IRequestHandler<GetCategoriesByUserQuery, Result<GetCategoriesByUserQueryResult>>
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly ISecurityContextAccessor _securityContextAccessor;
        private readonly UserManager<Core.Entities.User> _userManager;

        public GetCategoriesByUserQueryHandler(ICategoryRepository categoryRepository, ISecurityContextAccessor securityContextAccessor, UserManager<Core.Entities.User> userManager)
        {
            _categoryRepository = categoryRepository;
            _securityContextAccessor = securityContextAccessor;
            _userManager = userManager;
        }

        public async Task<Result<GetCategoriesByUserQueryResult>> Handle(GetCategoriesByUserQuery request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByIdAsync(_securityContextAccessor.UserId.ToString());

            if(user is null)
                return Result<GetCategoriesByUserQueryResult>.Error("Kullanıcı bulunamadı!", (int)HttpStatusCode.NotFound);

            var categories = await _categoryRepository.GetAllCategoriesByUser(user.Id)
                .Where(x => x.ExpenseType == request.ExpenseType)
                .Select(x => new CategoryModel
                {
                    Id = x.Id,
                    Title = x.Title,
                    ImageUrl = x.ImageUrl
                }).ToListAsync();

            if (categories is null || !categories.Any())
                return Result<GetCategoriesByUserQueryResult>.Error("Kullanıcıya ait kategori bulunamadı!", (int)HttpStatusCode.NotFound);

            return Result<GetCategoriesByUserQueryResult>.Success(new GetCategoriesByUserQueryResult { Categories = categories }, 
                (int)HttpStatusCode.OK);
        }
    }
}
