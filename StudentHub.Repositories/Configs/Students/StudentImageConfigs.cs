using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StudentHub.Models.Students;
using StudentHub.Repositories.Extensions;

namespace StudentHub.Repositories.Configs.Students
{
    public class StudentImageConfigs : IEntityTypeConfiguration<StudentImage>
    {
        public void Configure(EntityTypeBuilder<StudentImage> builder)
        {
            builder.ToTable("StudentImages");
            builder.HasKey(a => a.Id);
            builder.Property(a => a.StudentId).IsRequired(); 
            builder.Property(a => a.Type).IsRequired(); ;
            builder.Property(a => a.PhotoUrl).IsRequired(); ;
            builder.Property(a => a.Active);
            builder.Property(a => a.CreateDate).IsCreateDate().IsRequired();
            builder.HasOne(a => a.Student).WithMany(x => x.StudentImages).OnDelete(DeleteBehavior.Restrict).HasForeignKey(x => x.StudentId); 
        }
    }
}
