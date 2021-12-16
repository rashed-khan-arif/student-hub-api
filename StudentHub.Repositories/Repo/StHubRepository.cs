using Microsoft.Extensions.Logging;
using StudentHub.Repositories.Core;
using StudentHub.Repositories.Repo.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace StudentHub.Repositories.Repo
{
    public class StHubRepository<TModel> : BaseRepository<TModel>, IStHubRepository<TModel> where TModel : class
    {
        public StHubRepository(StudentHUBDbContext dbContext, ILogger<IStHubRepository<TModel>> logger) : base(dbContext, logger)
        {

        }

    }
}
