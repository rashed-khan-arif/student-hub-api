using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks; 
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query; 
using System.Threading;
using StudentHub.Repositories.Repo.Interfaces; 
using StudentHub.Models.Common;
using EFCoreSecondLevelCacheInterceptor; 

namespace StudentHub.Repositories.Repo
{
    public class BaseQueryable<TModel> : IBaseQueryable<TModel> where TModel : class
    {
        internal readonly WeakReference<System.Linq.IQueryable<TModel>> Query;

        public BaseQueryable(IQueryable<TModel> query)
        {
            Console.WriteLine("Base query..");
            Query = new WeakReference<IQueryable<TModel>>(query, false);
        }

        IIncludableBaseQueryable<TModel, TProperty> IBaseQueryable<TModel>.Include<TProperty>(Expression<Func<TModel, TProperty>> property)
        {
            if (Query.TryGetTarget(out var q))
            {
                var qq = q.Include(property);
                return new IncludableBaseQueryable<TModel, TProperty>(qq);
            }
            throw new InvalidOperationException();
        }

        public IBaseQueryable<TModel> Include(string property)
        {
            if (Query.TryGetTarget(out var q))
            {
                Query.SetTarget(q.Include(property));
            }
            return this;
        }

        public System.Linq.IQueryable<TModel> InternalQuery()
        {
            return Query.TryGetTarget(out var q) ? q : null;
        }

        public IBaseQueryable<TModel> Where(Expression<Func<TModel, bool>> expression)
        {
            if (Query.TryGetTarget(out var q))
            {
                Query.SetTarget(q.Where(expression));
            }
            return this;
        }


        public IBaseQueryable<TResult> Select<TResult>(Expression<Func<TModel, TResult>> selector) where TResult : class
        {
            if (Query.TryGetTarget(out var q))
            {
                return new BaseQueryable<TResult>(q.Select(selector));
            }
            return null;
        }

        public IBaseQueryable<TModel> Cacheable()
        {
            Console.WriteLine("Cachable...");
            return Cacheable(TimeSpan.FromHours(1));
        }

        public IBaseQueryable<TModel> Cacheable(TimeSpan timeToLive)
        {
            if (Query.TryGetTarget(out var q))
            {
                Query.SetTarget(q.Cacheable(CacheExpirationMode.Sliding, timeToLive, new EFCacheDebugInfo { IsCacheHit = true }));
            }
            return this;
        }

        public IBaseQueryable<TModel> WhereLike<T, TKey>(IEnumerable<T> values, Expression<Func<TModel, TKey>> propertyExpression)
        {
            ParameterExpression param = Expression.Parameter(typeof(TModel), "x");
            Expression property = Expression.PropertyOrField(param, (propertyExpression.Body as MemberExpression)?.Member.Name ?? throw new InvalidOperationException());

            Expression result = null;

            foreach (var s in values)
            {
                var method = typeof(DbFunctionsExtensions).GetMethod("Like", new[] { typeof(DbFunctions), typeof(string), typeof(string) });

                var tempResult1 = Expression.Call(
                    method,
                    Expression.Constant(EF.Functions),
                    property,
                    Expression.Constant($"%{s}%"));

                if (result == null)
                {
                    result = tempResult1;
                    continue;
                }

                result = Expression.OrElse(result, tempResult1);
            }

            return Where(Expression.Lambda<Func<TModel, bool>>(result ?? throw new InvalidOperationException(), param));
        }

        public async Task<int> CountAsync(Expression<Func<TModel, bool>> expression = null)
        {
            if (Query.TryGetTarget(out var q))
            {
                if (expression == null)
                    return await q.CountAsync();
                return await q.CountAsync(expression);
            }

            return await Task.FromResult(0);
        }
        public int Count(Expression<Func<TModel, bool>> expression = null)
        {
            if (Query.TryGetTarget(out var q))
            {
                if (expression == null)
                    return q.Count();
                return q.Count(expression);
            }

            return 0;
        }
        public IBaseQueryable<TModel> OrderBy(bool @ascending = true, params string[] properties)
        {
            if (Query.TryGetTarget(out var q))
            {
                Query.SetTarget(q.OrderBy(properties.Select(a => new SortModel
                {
                    PropertyName = a,
                    Sort = ascending ? "asc" : "desc"
                })));
                return this;
            }
            throw new InvalidOperationException();
        }

        public IBaseQueryable<TModel> OrderBy<TKey>(Expression<Func<TModel, TKey>> selectorKey, bool @ascending = true)
        {
            if (Query.TryGetTarget(out var q))
            {
                Query.SetTarget(@ascending ? q.OrderBy(selectorKey) : q.OrderByDescending(selectorKey));
                return this;
            }
            throw new InvalidOperationException();
        }

        public async Task<IList<TModel>> ToListAsync()
        {
            Console.WriteLine("Country list...");
            if (Query.TryGetTarget(out var q))
            {
                var result = await q.AsNoTracking().ToListAsync();

                return result;
            }

            throw new InvalidOperationException();
        }
        public virtual async Task<List<TModel>> ToListAsync(bool track = false, CancellationToken token = default)
        {
            if (Query.TryGetTarget(out var q))
            {
                return await (track ? q.ToListAsync(token) : q.AsNoTracking().ToListAsync(token));
            }

            return await Task.FromResult((List<TModel>)null);
        }
        public async Task<TModel> SingleAsync()
        {
            if (Query.TryGetTarget(out var q))
            {
                var result = await q.AsNoTracking().FirstOrDefaultAsync();

                return result;
            }
            throw new InvalidOperationException();
        }

        public TModel Single()
        {
            if (Query.TryGetTarget(out var q))
            {
                var result = q.FirstOrDefault();

                return result;
            }
            throw new InvalidOperationException();
        }

        public async Task<TModel> SingleAsync(Expression<Func<TModel, bool>> expression, bool track = false)
        {
            if (Query.TryGetTarget(out var q))
            {
                var result = track
                    ? await q.FirstOrDefaultAsync(expression)
                    : await q.AsNoTracking().FirstOrDefaultAsync(expression);

                return result;
            }

            return await Task.FromResult((TModel)null);
        }

        public async Task<PagedList<TModel>> ToPagedListAsync(int page, int pageSize, string primaryKey = null, bool @ascending = true)
        {
            if (!Query.TryGetTarget(out var q))
                throw new InvalidOperationException();

            var count = await CountAsync();
            //Query.TryGetTarget(out q);

            if (!string.IsNullOrEmpty(primaryKey))
            {
                q = q.OrderBy(new List<SortModel>
                {
                    new SortModel
                    {
                        PropertyName = primaryKey,
                        Sort = @ascending ? "asc": "desc"
                    }
                });


            }
            
            q = q.Skip((page - 1) * pageSize).Take(pageSize);

            var paged = new PagedList<TModel>
            {
                Items = await q.AsNoTracking().ToListAsync(),
                Page = page,
                PageSize = pageSize,
                TotalCount = count
            };

            return paged;

        }

        public IBaseQueryable<TModel> Where(bool ifCondition, Expression<Func<TModel, bool>> expression)
        {
            throw new NotImplementedException();
        }

        public IBaseQueryable<TModel> WhereWhen(bool condition, Expression<Func<TModel, bool>> expression)
        {
            if (!condition) return this;
            return Where(expression);
        }

    }
}
