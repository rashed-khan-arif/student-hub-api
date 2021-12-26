using StudentHub.Models.Auth;
using StudentHub.Models.Common;
using StudentHub.Models.Enum;
using StudentHub.Models.Network;
using System.ComponentModel.DataAnnotations;
using static StudentHub.Models.Students.Student;

namespace StudentHub.Models.Students
{
    public class Student
    {
        public int Id { get; set; }
        [Required]
        public int DistrictId { get; set; }
        [Required]
        public int UserId { get; set; }
        [Required]
        public StudentStatus Status { get; set; }
        public string Address { get; set; }
        public DateTime CreateDate { get; set; }        
        public virtual District District { get; set; }
        public virtual User User { get; set; }
        public virtual ICollection<StudentHubModel> StudentHubs { get; }
        public virtual ICollection<StudentImage> StudentImages { get; }
        public virtual ICollection<StudentInstitution> StudentInstitutions { get; }

        public Response Convert()
        {
            return new Response
            {
                Address = Address,
                DistrictId = DistrictId,
                Id = Id,
                Status = Status.ToString(),
                UserId = UserId,
                DistrictName = District.Name ?? "",
                FirstName = User?.FirstName ?? "",
                LastName = User?.LastName ?? "",
                Phone = User?.PhoneNumber ?? "",
                Email = User?.Email ?? "",
            };
        }

        public class Response
        {
            public int Id { get; set; }
            public int DistrictId { get; set; }
            public int UserId { get; set; }
            public string DistrictName { get; set; }
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public string Email { get; set; }
            public string Phone { get; set; }
            public string Status { get; set; }
            public string Address { get; set; }
        }
    }

}