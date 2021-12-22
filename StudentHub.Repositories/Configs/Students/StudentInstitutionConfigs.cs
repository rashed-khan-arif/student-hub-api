using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StudentHub.Models.Students;
using StudentHub.Repositories.Extensions;

namespace StudentHub.Repositories.Configs.Students
{
    public class StudentInstitutionConfigs : IEntityTypeConfiguration<StudentInstitution>
    {
        public void Configure(EntityTypeBuilder<StudentInstitution> builder)
        {
            builder.ToTable("StudentInstitutions");
            builder.HasKey(a => a.Id);
            builder.Property(a => a.StudentId).IsRequired(); 
            builder.Property(a => a.InstitutionId).IsRequired(); ;
            builder.Property(a => a.StudentClass).IsRequired(); ;
            builder.Property(a => a.Active);
            builder.Property(a => a.CreateDate).IsCreateDate().IsRequired();
            builder.HasOne(a => a.Institution).WithMany(x => x.StudentInstitutions).OnDelete(DeleteBehavior.Restrict).HasForeignKey(x => x.StudentId); 
            builder.HasOne(a => a.Student).WithMany(x => x.StudentInstitutions).OnDelete(DeleteBehavior.Restrict).HasForeignKey(x => x.StudentId); 
        }
    }
}
