using BudgetTracking.Common.Result;
using Microsoft.AspNetCore.Mvc;

namespace BudgetTracking.Common.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class BaseController
    {
        public IActionResult FromResult<T>(BaseResult<T> result)
            => new ObjectResult(result)
            {
                StatusCode = result.HttpStatusCode
            };
    }
}
