using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BudgetTracking.Data.Extensions.Linq
{
    public static class DynamicLinqExtensions
    {
        /// <summary>
        /// Koşullu kısıtlamalarda kullanılır - Search endpointleri gibi
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="source"></param>
        /// <param name="condition"></param>
        /// <param name="expression"></param>
        /// <returns></returns>
        public static IQueryable<TEntity> WhereIf<TEntity>(this IQueryable<TEntity> source,
            bool condition,
            Expression<Func<TEntity, bool>> expression)
        {
            if(condition)
                return source.Where(expression);

            return source;
        }
    }
}
