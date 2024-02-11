using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace BudgetTracking.Data.Extensions.Collection
{
    public static class CollectionExtensions
    {
        public static bool IsNullOrNotAny<TEntity>(this IEnumerable<TEntity> source)
          => source is null || !source.Any();
    }
}
