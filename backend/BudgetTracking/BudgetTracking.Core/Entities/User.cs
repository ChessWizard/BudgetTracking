using BudgetTracking.Common.Entity.Interfaces;
using BudgetTracking.Core.Enums;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BudgetTracking.Core.Entities
{
    public class User : IdentityUser<Guid>, IAuditEntity, ISoftDeleteEntity
    {
        public string? Name { get; set; }

        public string? Surname { get; set; }

        public GenderType GenderType { get; set; }

        public DateOnly BirthDate { get; set; }

        public AccountType AccountType { get; set; }
        public UserState UserState { get; set; }
        public DateTimeOffset CreatedOn { get; set; }
        public DateTimeOffset? ModifiedOn { get; set; }
        public bool IsDeleted { get; set; }

        public ICollection<UserRefreshToken> UserRefreshToken { get; set; }
    }
}
