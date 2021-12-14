namespace StudentHub.Models.Auth
{
    public class RoleFeature
    {
        public int Id { get; set; }
        public string? FeatureId { get; set; }
        public int RoleId { get; set; }
        public virtual Role Role { get; set; }
    }
}