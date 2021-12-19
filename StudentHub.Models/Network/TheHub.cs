using StudentHub.Models.Network;
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
        public virtual ICollection<Institution> Institutions { get; set; }
    }
}
