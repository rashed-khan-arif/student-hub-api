using System;
using System.Collections.Generic;
using System.Text;

namespace StudentHub.Repositories.Repo.Interfaces
{
    public interface IStHubRepository<TModel> : IBaseRepository<TModel> where TModel : class
    {
    }
}
