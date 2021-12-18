using StudentHub.Models.Enum;

namespace StudentHub.Models.Students
{
    public class StudentImage
    {
        public int Id { get; set; }
        public int StudentId { get; set; }
        public string PhotoUrl { get; set; }
        public ImageType Type { get; set; }
        public bool Active { get; set; }
    }

}