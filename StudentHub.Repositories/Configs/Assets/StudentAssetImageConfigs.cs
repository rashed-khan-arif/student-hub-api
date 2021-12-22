using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StudentHub.Models.AssetExchanges;
using StudentHub.Repositories.Extensions;

namespace StudentHub.Repositories.Configs.Assets
{
    public class StudentAssetImageConfigs : IEntityTypeConfiguration<AssetImage>
    {
        public void Configure(EntityTypeBuilder<AssetImage> builder)
        {
            builder.ToTable("AssetImages");
            builder.HasKey(a => a.Id);
            builder.Property(a => a.ImageTitle).IsRequired(); 
            builder.Property(a => a.ImageUrl).IsRequired(); 
            builder.Property(a => a.AssetId).IsRequired();   
            builder.Property(a => a.CreateDate).IsCreateDate().IsRequired();
            builder.HasOne(a => a.Asset).WithMany(x => x.AssetImages).OnDelete(DeleteBehavior.Restrict).HasForeignKey(x => x.AssetId);
        }
    }
}
