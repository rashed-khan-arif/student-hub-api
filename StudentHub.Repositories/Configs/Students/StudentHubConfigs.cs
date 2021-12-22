using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StudentHub.Models.Students;
using StudentHub.Repositories.Extensions;

namespace StudentHub.Repositories.Configs.Students
{
    public class StudentHubConfigs : IEntityTypeConfiguration<StudentHubModel>
    {
        public void Configure(EntityTypeBuilder<StudentHubModel> builder)
        {
            builder.ToTable("StudentHubs");
            builder.HasKey(a => a.Id);
            builder.Property(a => a.StudentId).IsRequired();
            builder.Property(a => a.HubId).IsRequired();
            builder.Property(a => a.Active);
            builder.Property(a => a.CreateDate).IsCreateDate().IsRequired();
            builder.HasOne(a => a.Student).WithMany(x => x.StudentHubs).OnDelete(DeleteBehavior.Restrict).HasForeignKey(x => x.StudentId);
            builder.HasOne(a => a.Hub).WithMany(x => x.StudentHubs).OnDelete(DeleteBehavior.Restrict).HasForeignKey(x => x.HubId);
        }
    }
}
