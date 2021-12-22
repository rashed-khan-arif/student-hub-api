using StudentHub.Models.Common;
using StudentHub.Models.Network;
using StudentHub.Models.Students;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentHub.Models.Network
{
    public class TheHub
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int DistrictId { get; set; }
        public bool Active { get; set; }
        public DateTime CreateDate { get; set; }
        public virtual ICollection<Institution> Institutions { get; set; }
        public virtual ICollection<StudentHubModel> StudentHubs { get; set; }
        public virtual District District { get; set; }
    }
}
