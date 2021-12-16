using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using StudentHub.Repositories.Repo;
using StudentHub.Repositories.Repo.Interfaces; 

namespace StudentHub.Repositories.Extensions
{
    public static class IncludableExtensions
    {
        public static IIncludableBaseQueryable<TModel, TKey> ThenInclude<TModel, TProperty, TKey>(
            this IIncludableBaseQueryable<TModel, List<TProperty>> query, Expression<Func<TProperty, TKey>> lstProperty)
            where TModel : class where TProperty : class where TKey : class
        {
            var qu = ((IncludableBaseQueryable<TModel, List<TProperty>>) query).Query;

            if (qu.TryGetTarget(out var q))
            {
                var qq = (IIncludableQueryable<TModel, List<TProperty>>) q;
                var th = qq.ThenInclude<TModel, TProperty, TKey>(lstProperty);

                return new IncludableBaseQueryable<TModel, TKey>(th);
            }

            throw new InvalidOperationException();
        }
    }
}