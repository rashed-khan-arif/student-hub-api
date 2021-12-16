using StudentHub.Models.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions; 

namespace StudentHub.Repositories.Extensions
{
    public static class OrderByExtensions
    {
        public static IQueryable<T> OrderBy<T>(this IQueryable<T> source, IEnumerable<SortModel> sortModels)
        {
            var expression = source.Expression;
            var count = 0;
            foreach (var item in sortModels)
            {
                var parameter = Expression.Parameter(typeof(T), "x");

                Expression selector;
                var props = item.PropertyName.Split(".");
                if (props.Length > 1)
                {
                    Expression body = parameter;
                    foreach (var prop in props)
                    {
                        body = Expression.PropertyOrField(body, prop);
                    }
                    selector = body;
                }
                else
                {
                    selector = Expression.PropertyOrField(parameter, item.PropertyName);
                }

                var method = string.Equals(item.Sort, "desc", StringComparison.OrdinalIgnoreCase) ?
                    (count == 0 ? "OrderByDescending" : "ThenByDescending") :
                    (count == 0 ? "OrderBy" : "ThenBy");

                expression = Expression.Call(
                    typeof(Queryable),
                    method,
                    new[]
                    {
                        source.ElementType,
                        selector.Type
                    }, expression,
                    Expression.Quote(Expression.Lambda(selector, parameter)));

                count++;
            }
            return count > 0 ? source.Provider.CreateQuery<T>(expression) : source;
        }
    }
}
