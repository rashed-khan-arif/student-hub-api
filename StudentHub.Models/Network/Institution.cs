 using StudentHub.Models.Enum;
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
    }
}
  