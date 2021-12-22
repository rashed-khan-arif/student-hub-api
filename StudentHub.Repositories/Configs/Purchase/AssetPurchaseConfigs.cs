using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StudentHub.Models.Purchase;
using StudentHub.Repositories.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentHub.Repositories.Configs.Purchase
{
    public class AssetPurchaseConfigs : IEntityTypeConfiguration<AssetPurchase>
    {
        public void Configure(EntityTypeBuilder<AssetPurchase> builder)
        {
            builder.ToTable("AssetPurchases");
            builder.HasKey(a => a.Id);
            builder.Property(a => a.PurchaseAmount).IsRequired();
            builder.Property(a => a.UserId).IsRequired();
            builder.Property(a => a.Vat).IsRequired();
            builder.Property(a => a.Tax).IsRequired(); 
            builder.Property(a => a.PurchaseDate).IsCreateDate().IsRequired();
            builder.HasOne(a => a.User).WithMany(x => x.AssetPurchases).OnDelete(DeleteBehavior.Restrict).HasForeignKey(x => x.UserId);
        }
    }
}
