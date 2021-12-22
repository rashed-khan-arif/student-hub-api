using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StudentHub.Models.Purchase;
using StudentHub.Repositories.Extensions;

namespace StudentHub.Repositories.Configs.Purchase
{
    public class PurchaseItemConfigs : IEntityTypeConfiguration<PurchaseItem>
    {
        public void Configure(EntityTypeBuilder<PurchaseItem> builder)
        {
            builder.ToTable("PurchaseItems");
            builder.HasKey(a => a.Id);
            builder.Property(a => a.AssetId).IsRequired(); 
            builder.Property(a => a.AssetPurchaseId).IsRequired(); 
            builder.Property(a => a.Quantity).IsRequired(); 
            builder.Property(a => a.SalesPrice).IsRequired(); 
            builder.Property(a => a.CreateDate).IsCreateDate().IsRequired();
            builder.HasOne(a => a.Asset).WithMany(x => x.PurchaseItems)
                .OnDelete(DeleteBehavior.Restrict)
                .HasForeignKey(x => x.AssetId);
            builder.HasOne(a => a.AssetPurchase).WithMany(x => x.PurchaseItems)
               .OnDelete(DeleteBehavior.Restrict)
               .HasForeignKey(x => x.AssetPurchaseId);
        }
    }
}
