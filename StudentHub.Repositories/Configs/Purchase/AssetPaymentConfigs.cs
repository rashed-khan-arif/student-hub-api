using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StudentHub.Models.Payment;
using StudentHub.Repositories.Extensions;

namespace StudentHub.Repositories.Configs.Purchase
{
    public class AssetPaymentConfigs : IEntityTypeConfiguration<AssetPayment>
    {
        public void Configure(EntityTypeBuilder<AssetPayment> builder)
        {
            builder.ToTable("AssetPayments");
            builder.HasKey(a => a.Id);
            builder.Property(a => a.PurchaseId).IsRequired();
            builder.Property(a => a.PaymentAmount).IsRequired();
            builder.Property(a => a.PaymentStatus).IsRequired();
            builder.Property(a => a.PaymentBy).IsRequired(); 
            builder.Property(a => a.PaymentDate).IsCreateDate().IsRequired();
            builder.HasOne(a => a.PaymentUser).WithMany(x => x.AssetPayments).HasForeignKey(x => x.PaymentBy);
            builder.HasOne(a => a.AssetPurchase).WithMany(x => x.AssetPayments)
                .OnDelete(DeleteBehavior.Restrict)
                .HasForeignKey(x => x.PurchaseId);          
            
        }
    }
}
