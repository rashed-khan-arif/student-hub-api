using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StudentHub.Models.Students;
using StudentHub.Repositories.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentHub.Repositories.Configs.Students
{
    public class StudentConfigs : IEntityTypeConfiguration<Student>
    {
        public void Configure(EntityTypeBuilder<Student> builder)
        {
            builder.ToTable("Students");
            builder.HasKey(a => a.Id);
            builder.Property(a => a.UserId).IsRequired();
            builder.Property(a => a.Status).IsRequired();
            builder.Property(a => a.Address);
            builder.Property(a => a.DistrictId);
            builder.Property(a => a.CreateDate).IsCreateDate().IsRequired();
            builder.HasOne(a => a.District).WithMany(x => x.Students).OnDelete(DeleteBehavior.Restrict).HasForeignKey(x => x.DistrictId);
        }
    }
}
