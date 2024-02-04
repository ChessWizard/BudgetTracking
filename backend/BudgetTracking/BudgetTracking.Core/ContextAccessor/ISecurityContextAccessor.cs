using BudgetTracking.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BudgetTracking.Core.ContextAccessor
{
    public interface ISecurityContextAccessor
    {
        public Guid TokenId { get; }

        public Guid UserId { get; }

        public string Email { get; }

        public UserState UserState { get; }
    }
}
