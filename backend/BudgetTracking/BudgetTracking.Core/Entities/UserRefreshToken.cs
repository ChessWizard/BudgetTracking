using BudgetTracking.Common.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BudgetTracking.Core.Entities
{
    public class UserRefreshToken : BaseEntity<Guid>
    {
        public string Code { get; set; }

        public DateTimeOffset Expiration { get; set; }

        public Guid UserId { get; set; }

        public User User { get; set; }
    }
}
