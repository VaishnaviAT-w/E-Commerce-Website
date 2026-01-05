using System;
using System.Linq;
using System.Linq.Expressions;

namespace E_Commerce_Website.Data.Extensions
{
    public static class QueryableExtensions
    {
        /// <summary>
        /// Filters the IQueryable only if the condition is true
        /// </summary>
        public static IQueryable<T> WhereIf<T>(
            this IQueryable<T> source,
            bool condition,
            Expression<Func<T, bool>> predicate)
        {
            return condition ? source.Where(predicate) : source;
        }
    }
}
