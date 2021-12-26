using StudentHub.Models.Students;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentHub.Supervisor.Interfaces
{
    public interface IStudentService : IStudentHubDataService<Student>
    {
    }
}
