using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace MyEntityModel
{
    public static class QueryExtensions
    {
        /// <summary>
        /// Dynamic Sorting with Linq
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source"></param>
        /// <param name="propertyName">Field name to order by</param>
        /// <param name="sortDirection">ASC or DESC</param>
        /// <returns></returns>
        public static IQueryable<T> SortBy<T>(this IQueryable<T> source, string propertyName, string sortDirection)
        {
            if (source == null)
            {
                throw new ArgumentNullException("source");
            }

            // DataSource control passes the sort parameter with a direction
            // if the direction is descending         


            if (String.IsNullOrEmpty(propertyName))
            {
                return source;
            }

            ParameterExpression parameter = Expression.Parameter(source.ElementType, String.Empty);
            MemberExpression property = Expression.Property(parameter, propertyName);
            LambdaExpression lambda = Expression.Lambda(property, parameter);

            string methodName = (sortDirection == "ASC") ? "OrderBy" : "OrderByDescending";

            Expression methodCallExpression = Expression.Call(typeof(Queryable), methodName,
                                                new Type[] { source.ElementType, property.Type },
                                                source.Expression, Expression.Quote(lambda));

            return source.Provider.CreateQuery<T>(methodCallExpression);
        }
    }
}