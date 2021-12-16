using Microsoft.EntityFrameworkCore;
using StudentHub.Repositories.Common;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks; 

namespace StudentHub.Repositories.Repo.Interfaces
{
    public interface IBaseRepository<TModel>:IDisposable where TModel:class
    {
        Task<TModel> GetByIdAsync(object id);
        IBaseQueryable<TModel> Query { get; }
        Task<bool> InsertAsync(TModel model, bool recursive = true);
        Task<bool> InsertRangeAsync(ICollection<TModel> models, bool recursive = true);
        Task<bool> InsertWithTransactionAsync(TModel model, bool recursive = true);
        bool InsertRangeWithTransaction(ICollection<TModel> models, bool recursive = true);
        Task<bool> InsertRangeWithTransactionAsync(ICollection<TModel> models, bool recursive = true);
        Task<bool> UpdateAsync(object id, TModel model, bool recursive = true, params string[] excludeProperties);
        bool UpdateOnly(object id, TModel model, params string[] properties);
        Task<bool> UpdateOnlyAsync(object id, TModel model, params string[] properties);
        Task<bool> UpdateRangeAsync(List<TModel> models, params string[] excludeProperties);
        Task<bool> UpdateOnlyRangeAsync(List<TModel> models, params string[] properties);
        bool UpdateOnlyRange(List<TModel> models, params string[] properties);
        Task<bool> UpdateWithTransactionAsync(object id, TModel model,bool recursive = true, params string[] excludeProperties);
        Task<bool> DeleteAsync(int id);
        DbContext InternalCTX();
        Task<int[]> ExecuteDbProcedureAsync(DbProcedure procedure, params string[] args);




    }
}
