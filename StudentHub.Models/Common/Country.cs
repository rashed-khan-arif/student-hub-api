using StudentHub.Models.Network;
using StudentHub.Models.Students;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentHub.Models.Common
{
    public class Country
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
    }

    //Ex.Barisal Dhaka Rajshahi
    public class City
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int CountryId { get; set; }

    }

    public class District
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int CityId { get; set; }
        public virtual ICollection<TheHub> Hubs { get; set; }
        public virtual ICollection<Student> Students { get; set; }
    }

   
}
