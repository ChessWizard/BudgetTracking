using BudgetTracking.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BudgetTracking.Service.Services.User.Queries.GetUserDetails
{
    public class GetUserDetailsQueryResult
    {
        public string? Name { get; set; }
        public string? Surname { get; set; }
        public string Email { get; set; }
        public GenderType GenderType { get; set; }
        public DateOnly BirthDate { get; set; }
        public AccountType AccountType { get; set; }
        public UserState UserState { get; set; }
        public DateTimeOffset CreatedOn { get; set; }
        public DateTimeOffset? ModifiedOn { get; set; }
        public bool IsDeleted { get; set; }
    }
}
