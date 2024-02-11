using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BudgetTracking.Core.Enums
{
    public enum PaymentType
    {
        None = 0,// random kendi yarattığı bir para topluluğuysa
        Wallet = 1,
        Bank = 2,
        Credit = 3
    }
}
