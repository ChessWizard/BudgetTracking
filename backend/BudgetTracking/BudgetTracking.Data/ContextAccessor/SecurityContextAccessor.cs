using BudgetTracking.Core.ContextAccessor;
using BudgetTracking.Core.Enums;
using BudgetTracking.Data.Constants;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace BudgetTracking.Data.ContextAccessor
{
    public class SecurityContextAccessor : ISecurityContextAccessor
    {
        private readonly IHttpContextAccessor _contextAccessor;

        public SecurityContextAccessor(IHttpContextAccessor contextAccessor)
        {
            _contextAccessor = contextAccessor;
        }

        public Guid TokenId => Guid.TryParse(_contextAccessor.HttpContext?.User?.FindFirstValue(JwtRegisteredClaimNames.Jti), out Guid result) ? result : Guid.NewGuid();
        public Guid UserId => Guid.TryParse(_contextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.NameIdentifier), out Guid result) ? result : Guid.NewGuid();
        public string Email => _contextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.Email);
        public UserState UserState => (UserState)Enum.Parse(typeof(UserState), _contextAccessor.HttpContext?.User.FindFirstValue(JwtClaimConstants.UserState));
    }
}
