using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentHub.Supervisor.Interfaces
{
    public interface IStudentHubDataService<T>
    {
        Task<T> Get(int id);
        Task<ICollection<T>> GetAll();
        Task<bool> Insert(T entity);
        Task<bool> Update(T entity);
    }
}
