using StudentHub.Models.Common;
using StudentHub.Models.Network;

namespace StudentHub.Models.Students
{
    public class StudentHubModel
    {
        public int Id { get; set; }
        public int StudentId { get; set; }
        public int HubId { get; set; }
        public bool Active { get; set; }
        public DateTime CreateDate { get; set; }
        public virtual Student Student { get; set; }
        public virtual TheHub Hub { get; set; }
    }

}