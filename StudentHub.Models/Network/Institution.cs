using StudentHub.Models.Common;
using StudentHub.Models.Enum;
using StudentHub.Models.Students;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentHub.Models.Network
{
    public class Institution
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int HubId { get; set; }
        public InstitutionType InstitutionType { get; set; }
        public InstitutionManagementType ManagementType { get; set; }
        public int EIIN { get; set; }
        public string ManagementName { get; set; }
        public DateTime CreateDate { get; set; }
        public virtual TheHub Hub { get; set; }
        public virtual ICollection<StudentInstitution> StudentIntitutions { get; set; }
    }
}
  