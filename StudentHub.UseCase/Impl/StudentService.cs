using StudentHub.Models.Students;
using StudentHub.Repositories.Repo.Interfaces;
using StudentHub.Supervisor.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentHub.Supervisor.Impl
{
    public class StudentService : IStudentService
    {
        private readonly IStHubRepository<Student> _repo;

        public StudentService(IStHubRepository<Student> repo)
        {
            _repo = repo;
        }

        public async Task<ICollection<Student>> GetAll()
        {
            return await _repo.Query.Include(x => x.User).ToListAsync();
        }

        public async Task<Student> Get(int id)
        {
            return await _repo.Query
                .Include(x => x.District)
                .Include(x => x.User)
                .Where(x => x.Id == id).SingleAsync();
        }

        public async Task<bool> Insert(Student entity)
        {
            return await _repo.InsertAsync(entity, false);
        }

        public async Task<bool> Update(Student entity)
        {
            return await _repo.UpdateAsync(entity.Id, entity);
        }
    }
}
