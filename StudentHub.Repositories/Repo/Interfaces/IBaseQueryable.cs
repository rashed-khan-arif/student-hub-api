using StudentHub.Models.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks; 

namespace StudentHub.Repositories.Repo.Interfaces
{
    public interface IBaseQueryable<TModel> where TModel : class
    {
        IIncludableBaseQueryable<TModel, TProperty> Include<TProperty>(Expression<Func<TModel, TProperty>> property) where TProperty : class;
        IBaseQueryable<TModel> Include(string property);
        IQueryable<TModel> InternalQuery();
        IBaseQueryable<TModel> Where(Expression<Func<TModel, bool>> expression);
        IBaseQueryable<TModel> Where(bool ifCondition, Expression<Func<TModel, bool>> expression);
        IBaseQueryable<TModel> WhereWhen(bool condition, Expression<Func<TModel, bool>> expression);
        IBaseQueryable<TResult> Select<TResult>(Expression<Func<TModel, TResult>> selector) where TResult : class;
        IBaseQueryable<TModel> Cacheable();
        IBaseQueryable<TModel> Cacheable(TimeSpan timeToLive);
        IBaseQueryable<TModel> WhereLike<T, TKey>(IEnumerable<T> values, Expression<Func<TModel, TKey>> propertyExpression);
        int Count(Expression<Func<TModel, bool>> expression = null);
        Task<int> CountAsync(Expression<Func<TModel, bool>> expression = null);
        IBaseQueryable<TModel> OrderBy(bool ascending = true, params string[] properties);
        IBaseQueryable<TModel> OrderBy<TKey>(Expression<Func<TModel,TKey>>selectorKey ,bool ascending = true);
        Task<IList<TModel>> ToListAsync();
        Task<List<TModel>> ToListAsync(bool track = false, CancellationToken token = default);
        Task<TModel> SingleAsync();
        TModel Single();
        Task<TModel> SingleAsync(Expression<Func<TModel, bool>> expression, bool track = false);

        Task<PagedList<TModel>> ToPagedListAsync(int page, int pageSize, string primaryKey = null, bool ascending = true);
     
    }
}
