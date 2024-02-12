using Microsoft.EntityFrameworkCore;
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

        /// <summary>
        /// Tek seferde birden fazla tabloyu include etmeye yarar
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="query"></param>
        /// <param name="includes"></param>
        /// <returns></returns>
        public static IQueryable<T> IncludeMultiple<T>(this IQueryable<T> query, List<string> includes) where T : class
        {
            if (includes != null)
            {
                query = includes.Aggregate(query, (current, include) => current.Include(include));
            }

            return query;
        }

        /// <summary>
        /// Çoklu Include parametresi eklemeye yarar
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="query"></param>
        /// <param name="propertyName"></param>
        /// <returns></returns>
        private static IQueryable<T> Include<T>(this IQueryable<T> query, string propertyName) where T : class
        {
            var parameter = Expression.Parameter(typeof(T), "x");
            var property = Expression.Property(parameter, propertyName);
            var lambda = Expression.Lambda(property, parameter);
            var methodCallExpression = Expression.Call(typeof(EntityFrameworkQueryableExtensions), "Include", new Type[] { typeof(T), property.Type }, query.Expression, Expression.Quote(lambda));

            return query.Provider.CreateQuery<T>(methodCallExpression);
        }
    }
}
