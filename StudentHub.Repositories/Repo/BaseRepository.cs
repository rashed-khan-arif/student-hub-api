using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Data.Common;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
 
using Microsoft.Extensions.Logging;
using StudentHub.Repositories.Repo.Interfaces;
using StudentHub.Repositories.Common;

namespace StudentHub.Repositories.Repo
{
    public abstract class BaseRepository<TModel> : IBaseRepository<TModel> where TModel : class
    {
        protected readonly ILogger<IBaseRepository<TModel>> _logger;
        public DbContext _dbContext;
        private DbSet<TModel> _set;

        public BaseRepository(DbContext dbContext, ILogger<IBaseRepository<TModel>> logger)
        {
            _logger = logger;
            _dbContext = dbContext;
            _set = dbContext.Set<TModel>();
        }

        public virtual void Dispose()
        {
            _dbContext = null;
            _set = null;
        }

        public async Task<TModel> GetByIdAsync(object id)
        {
            return await _set.FindAsync(id);
        }

        public IBaseQueryable<TModel> Query => new BaseQueryable<TModel>(_set.AsQueryable());
        public IBaseQueryable<TNewModel> AltQuery<TNewModel>() where TNewModel : class => new BaseQueryable<TNewModel>(_dbContext.Set<TNewModel>().AsQueryable());
        public async Task<bool> InsertAsync(TModel model, bool recursive = true)
        {
            if (recursive)
            {
                _set.Add(model);
            }
            else
            {
                _dbContext.Entry(model).State = EntityState.Added;
            }

            return await _dbContext.SaveChangesAsync() > 0;
        }

        public async Task<bool> InsertRangeAsync(ICollection<TModel> models, bool recursive = true)
        {
            if (recursive)
            {
                _set.AddRange(models);
            }
            else
            {
                foreach (var model in models)
                {
                    _dbContext.Entry(model).State = EntityState.Added;
                }
            }
            return await _dbContext.SaveChangesAsync() >= models.Count;
        }

        public async Task<bool> InsertWithTransactionAsync(TModel model, bool recursive = true)
        {
            return await InsertRangeWithTransactionAsync(new List<TModel> { model }, recursive);
        }

        public bool InsertRangeWithTransaction(ICollection<TModel> models, bool recursive = true)
        {
            using (var trx = _dbContext.Database.BeginTransaction())
            {
                try
                {
                    if (recursive)
                    {
                        _set.AddRange(models);
                    }
                    else
                    {
                        foreach (var model in models)
                        {
                            _dbContext.Entry(model).State = EntityState.Added;
                        }
                    }

                    _dbContext.SaveChanges();
                    trx.Commit();
                    return true;
                }
                catch (Exception e)
                {
                    Debug.WriteLine(e);
                    trx.Rollback();
                    return false;
                }
            }
        }

        public async Task<bool> InsertRangeWithTransactionAsync(ICollection<TModel> models, bool recursive = true)
        {
            using (var trx = await _dbContext.Database.BeginTransactionAsync())
            {
                try
                {
                    if (recursive)
                    {
                        _set.AddRange(models);
                    }
                    else
                    {
                        foreach (var model in models)
                        {
                            _dbContext.Entry(model).State = EntityState.Added;
                        }
                    }

                    await _dbContext.SaveChangesAsync();
                    trx.Commit();
                    return true;
                }
                catch (Exception e)
                {
                    Debug.WriteLine(e);
                    trx.Rollback();
                    return false;
                }
            }
        }

        public async Task<bool> UpdateAsync(object id, TModel model, bool recursive = true, params string[] excludeProperties)
        {
            var item = await _set.FindAsync(id);
            if (item == null)
            {
                return await Task.FromResult(false);
            }

            _dbContext.Entry(item).State = EntityState.Detached;

            EntityEntry<TModel> entry;
            if (recursive)
            {
                entry = _dbContext.Update(model);
            }
            else
            {
                entry = _dbContext.Entry(model);
                entry.State = EntityState.Modified;
            }

            foreach (var property in excludeProperties)
            {
                entry.Property(property).IsModified = false;
            }

            return await _dbContext.SaveChangesAsync() > 0;

        }

        public bool UpdateOnly(object id, TModel model, params string[] properties)
        {
            var item = _set.Find(id);

            if (item == null)
            {
                return false;
            }

            _dbContext.Entry(item).State = EntityState.Detached;

            using (var trx = _dbContext.Database.BeginTransaction())
            {
                try
                {
                    var entry = _dbContext.Entry(model);

                    foreach (var property in properties)
                    {
                        entry.Property(property).IsModified = true;
                    }
                    var aff = _dbContext.SaveChanges();
                    trx.Commit();
                    return aff > 0;
                }
                catch (Exception)
                {
                    trx.Rollback();
                    throw;
                }
            }
        }

        public async Task<bool> UpdateOnlyAsync(object id, TModel model, params string[] properties)
        {
            var item = await _set.FindAsync(id);

            if (item == null)
            {
                return await Task.FromResult(false);
            }

            _dbContext.Entry(item).State = EntityState.Detached;

            var entry = _dbContext.Entry(model);

            foreach (var property in properties)
            {
                entry.Property(property).IsModified = true;
            }
            return await _dbContext.SaveChangesAsync() > 0;
        }

        public async Task<bool> UpdateRangeAsync(List<TModel> models, params string[] excludeProperties)
        {
            foreach (var model in models)
            {
                var entry = _dbContext.Entry(model);
                entry.State = EntityState.Modified;

                foreach (var property in excludeProperties)
                {
                    entry.Property(property).IsModified = false;
                }
            }

            return await _dbContext.SaveChangesAsync() > 0;
        }

        public async Task<bool> UpdateOnlyRangeAsync(List<TModel> models, params string[] properties)
        {
            foreach (var model in models)
            {
                var entry = _dbContext.Entry(model);

                foreach (var property in properties)
                {
                    entry.Property(property).IsModified = true;
                }
            }

            return await _dbContext.SaveChangesAsync() > 0;
        }

        public  bool UpdateOnlyRange(List<TModel> models, params string[] properties)
        {
            foreach (var model in models)
            {
                var entry = _dbContext.Entry(model);

                foreach (var property in properties)
                {
                    entry.Property(property).IsModified = true;
                }
            }

            return  _dbContext.SaveChanges() > 0;
        }
        public async Task<bool> UpdateWithTransactionAsync(object id, TModel model, bool recursive = true, params string[] excludeProperties)
        {
            var item = await _set.FindAsync(id);

            if (item == null)
            {
                return await Task.FromResult(false);
            }

            _dbContext.Entry(item).State = EntityState.Detached;

            using (var trx = await _dbContext.Database.BeginTransactionAsync())
            {
                try
                {
                    EntityEntry<TModel> entry;
                    if (recursive)
                    {
                        entry = _dbContext.Update(model);
                    }
                    else
                    {
                        entry = _dbContext.Entry(model);
                        entry.State = EntityState.Modified;
                    }

                    foreach (var property in excludeProperties)
                    {
                        entry.Property(property).IsModified = false;

                    }

                    await _dbContext.SaveChangesAsync();
                    trx.Commit();
                    return true;
                }
                catch (Exception e)
                {
                    Debug.WriteLine(e);
                    trx.Rollback();
                    return false;
                }
            }


        }

        public async Task<bool> DeleteAsync(int id)
        {
            var item = await _set.FindAsync(id);
            if (item == null) return false;

            _dbContext.Remove(item);
            return await _dbContext.SaveChangesAsync() > 0;
        }

        public DbContext InternalCTX()
        {
            return _dbContext;
        }

        public async Task<int[]> ExecuteDbProcedureAsync(DbProcedure procedure, params string[] args)
        {
            if (args == null || procedure.Parameters.Length != args.Length)
            {
                throw new ArgumentOutOfRangeException(nameof(args), "Parameter and Argument size mismatch");
            }

            using (var command = _dbContext.Database.GetDbConnection().CreateCommand())
            {
                command.Parameters.Clear();
                command.CommandText = procedure.Name;
                command.CommandType = CommandType.StoredProcedure;

                for (var index = 0; index < args.Length; index++)
                {
                    var value = args[index];
                    var param = new SqlParameter
                    {
                        ParameterName = procedure.Parameters[index],
                        SqlDbType = SqlHelper.GetDbType(value.GetType()),
                        Value = value,
                        Direction = ParameterDirection.Input
                    };
                    command.Parameters.Add(param);
                }

                var ids = new List<int>();

                if (command.Connection.State != ConnectionState.Open)
                {
                    await command.Connection.OpenAsync();
                }

                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        ids.Add((int)reader[0]);
                    }
                }

                return ids.ToArray();
            }
        }


    }
}
