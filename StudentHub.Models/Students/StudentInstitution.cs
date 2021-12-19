using StudentHub.Models.Enum;
using StudentHub.Models.Network;

namespace StudentHub.Models.Students
{
    public class StudentInstitution
    {
        public int Id { get; set; }
        public int InstitutionId { get; set; }
        public int StudentId { get; set; }
        public StudentClass StudentClass { get; set; }
        public bool Active { get; set; }
        public DateTime CreateDate { get; set; }
        public virtual Institution Institution { get; set; }
        public virtual Student Student { get; set; }

    }

}