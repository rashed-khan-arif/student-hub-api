using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using StudentHub.Repositories.Repo;
using StudentHub.Repositories.Repo.Interfaces; 

namespace StudentHub.Repositories.Repo
{
   public class IncludableBaseQueryable<TModel, TProperty> : BaseQueryable<TModel>, IIncludableBaseQueryable<TModel, TProperty> where TModel : class where TProperty : class
    {
        public IncludableBaseQueryable(IIncludableQueryable<TModel,TProperty> query) : base(query)
        {
        }

        public IIncludableBaseQueryable<TModel, TKey> ThenInclude<TKey>(Expression<Func<TProperty, TKey>> property) where TKey : class
        {
            if (!Query.TryGetTarget(out var q))
            {
                throw new InvalidOperationException();
            }
            var qq = (IIncludableQueryable<TModel, TProperty>)q;
            var th = qq.ThenInclude<TModel, TProperty, TKey>(property);

            return new IncludableBaseQueryable<TModel, TKey>(th);
        }
        
    }
    public static class IncludableExtensions
    {
        public static IIncludableBaseQueryable<TModel, TKey> ThenInclude<TModel, TProperty, TKey>(this IIncludableBaseQueryable<TModel, List<TProperty>> query, Expression<Func<TProperty, TKey>> lstPproperty) where TModel : class where TProperty : class where TKey : class
        {
            var qu = ((IncludableBaseQueryable<TModel, List<TProperty>>)query).Query;

            if (qu.TryGetTarget(out var q))
            {
                var qq = (IIncludableQueryable<TModel, List<TProperty>>)q;
                var th = qq.ThenInclude<TModel, TProperty, TKey>(lstPproperty);

                return new IncludableBaseQueryable<TModel, TKey>(th);
            }

            throw new InvalidOperationException();
        }
    }
}
