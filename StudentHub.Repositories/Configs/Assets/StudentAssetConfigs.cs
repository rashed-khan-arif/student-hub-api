using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StudentHub.Models.AssetExchanges;
using StudentHub.Repositories.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentHub.Repositories.Configs.Assets
{
    public class StudentAssetConfigs : IEntityTypeConfiguration<StudentAsset>
    {
        public void Configure(EntityTypeBuilder<StudentAsset> builder)
        {
            builder.ToTable("StudentAssets");
            builder.HasKey(a => a.Id);
            builder.Property(a => a.Title).IsRequired(); 
            builder.Property(a => a.Description).IsRequired(); 
            builder.Property(a => a.Category).IsRequired(); 
            builder.Property(a => a.Type).IsRequired(); 
            builder.Property(a => a.UsedDuration).IsRequired(); 
            builder.Property(a => a.IsUsed).IsRequired(); 
            builder.Property(a => a.Price).IsRequired(); 
            builder.Property(a => a.Status).IsRequired(); 
            builder.Property(a => a.UserId).IsRequired(); 
            builder.Property(a => a.CreateDate).IsCreateDate().IsRequired();
            builder.HasOne(a => a.User).WithMany(x => x.StudentAssets).OnDelete(DeleteBehavior.Restrict).HasForeignKey(x => x.UserId);

        }
    }
}
