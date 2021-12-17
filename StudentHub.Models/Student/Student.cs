using StudentHub.Models.Network;

namespace StudentHub.Models.Student
{
    public class Student
    {
        public int Id { get; set; }
        public int DistrictId { get; set; }
        public StudentStatus Status { get; set; }
        public string Address { get; set; }

    }

    public class StudentImage
    {
        public int Id { get; set; }
        public int StudentId { get; set; }
        public string PhotoUrl { get; set; }
        public ImageType Type { get; set; }
        public bool Active { get; set; }
    }

    public enum ImageType
    {
        ProfilePhoto = 1,
        CoverPhoto = 2
    }
    public enum StudentStatus
    {
        Active = 1,
        Inactive = 2,
        Approved = 3,
        Rejected = 4,
        Verified = 5,
        Pending = 6
    }

    public class StudentHub
    {
        public int Id { get; set; }
        public int StudentId { get; set; }
        public int HubId { get; set; }
        public bool Active { get; set; }
    }

    public class StudentInstitution
    {
        public int Id { get; set; }
        public int InstitutionId { get; set; }
        public int StudentId { get; set; }
        public StudentClass StudentClass { get; set; }
        public bool Active { get; set; }
    }

}