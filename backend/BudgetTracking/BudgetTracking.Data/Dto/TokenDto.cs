using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BudgetTracking.Data.Dto
{
    public class TokenDto
    {
        public string AccessToken { get; set; }

        public DateTimeOffset AccessTokenExpiration { get; set; }

        public string RefreshToken { get; set; }

        public DateTimeOffset RefreshTokenExpiration { get; set; }
    }
}
