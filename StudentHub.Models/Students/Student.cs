using StudentHub.Models.Auth;
using StudentHub.Models.Enum;
using StudentHub.Models.Network;

namespace StudentHub.Models.Students
{
    public class Student
    {
        public int Id { get; set; }
        public int DistrictId { get; set; }
        public int UserId { get; set; }
        public StudentStatus Status { get; set; }
        public string Address { get; set; }
        public DateTime CreateDate { get; set; }
        public virtual User User { get; set; }
        public virtual ICollection<StudentHub> StudentHubs { get;}
        public virtual ICollection<StudentImage> StudentImages { get;}
    }

}