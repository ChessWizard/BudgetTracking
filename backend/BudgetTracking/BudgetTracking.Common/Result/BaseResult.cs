using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BudgetTracking.Common.Result
{
    public class BaseResult<TData>
    {
        public TData Data { get; set; }

        public string Message { get; set; }

        public int HttpStatusCode { get; set; }
    }
}
