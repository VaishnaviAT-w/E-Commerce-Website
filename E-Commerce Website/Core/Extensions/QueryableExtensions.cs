using System.Linq.Expressions;

namespace E_Commerce_Website.Data.Extensions
{
    public static class Extension

    {

        public static IQueryable<T> WhereIf<T>(this IQueryable<T> source, bool condition,
            Expression<Func<T, bool>> predicate) where T : class, new()

        {

            if (condition)

                return source.Where(predicate);

            else

                return source;

        }
    }
}