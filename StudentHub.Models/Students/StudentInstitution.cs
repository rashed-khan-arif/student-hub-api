using StudentHub.Models.Enum;

namespace StudentHub.Models.Students
{
    public class StudentInstitution
    {
        public int Id { get; set; }
        public int InstitutionId { get; set; }
        public int StudentId { get; set; }
        public StudentClass StudentClass { get; set; }
        public bool Active { get; set; }
    }

}