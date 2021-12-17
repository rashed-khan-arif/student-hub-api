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


    public enum InstitutionType
    {
        School = 1,
        College = 2,
        Madrasha = 3,
        Vocational = 4,
        SchoolAndCollenge = 5
    }
    public enum InstitutionManagementType
    {
        NonGovernment = 1,
        Government = 2,
        Missionary = 3,
        GovernmentPrimary = 4,
        LocalGovernment = 5,
        Autonomous = 6,
        Others = 7
    }

    public enum StudentClass
    {
        Six = 6,
        Seven = 7,
        Eight = 8,
        Nine = 9,
        Ten = 10,
        InterFirstYear = 11,
        InterSecondYear = 12,
        Honors = 13,
        Alem = 14,
        Fazel = 15,
        Kamil = 16
    }

}
