using System;
using System.Linq.Expressions;

namespace StudentHub.Repositories.Repo.Interfaces
{
    public interface IIncludableBaseQueryable<TModel, TProperty> : IBaseQueryable<TModel> where TModel : class where TProperty : class
    {
        IIncludableBaseQueryable<TModel, TKey> ThenInclude<TKey>(Expression<Func<TProperty, TKey>> property) where TKey : class;
    }
}
