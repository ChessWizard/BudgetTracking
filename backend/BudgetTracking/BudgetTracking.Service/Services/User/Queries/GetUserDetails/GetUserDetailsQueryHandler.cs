using BudgetTracking.Common.Result;
using BudgetTracking.Core.ContextAccessor;
using BudgetTracking.Core.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace BudgetTracking.Service.Services.User.Queries.GetUserDetails
{
    public class GetUserDetailsQueryHandler : IRequestHandler<GetUserDetailsQuery, Result<GetUserDetailsQueryResult>>
    {
        private readonly ISecurityContextAccessor _contextAccessor;
        private readonly UserManager<Core.Entities.User> _userManager;

        public GetUserDetailsQueryHandler(ISecurityContextAccessor contextAccessor, UserManager<Core.Entities.User> userManager)
        {
            _contextAccessor = contextAccessor;
            _userManager = userManager;
        }

        public async Task<Result<GetUserDetailsQueryResult>> Handle(GetUserDetailsQuery request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByIdAsync(_contextAccessor.UserId.ToString());

            if (user is null)
                return Result<GetUserDetailsQueryResult>.Error("Kullanıcı bulunamadı!", (int)HttpStatusCode.NotFound);

            GetUserDetailsQueryResult result = new()
            {
                AccountType = user.AccountType,
                BirthDate = user.BirthDate.GetValueOrDefault(),// değeri varsa değer alınır, yoksa default değer döner
                CreatedOn = user.CreatedOn,
                GenderType = user.GenderType.GetValueOrDefault(),
                IsDeleted = user.IsDeleted,
                ModifiedOn = user.ModifiedOn,
                Name = user.Name,
                Surname = user.Surname,
                UserState = user.UserState,
                Email = user.Email
            };

            return Result<GetUserDetailsQueryResult>.Success(result, (int)HttpStatusCode.OK);
        }
    }
}
