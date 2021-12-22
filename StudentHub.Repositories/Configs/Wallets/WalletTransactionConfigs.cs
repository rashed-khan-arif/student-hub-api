using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StudentHub.Models.Wallets;
using StudentHub.Repositories.Extensions;

namespace StudentHub.Repositories.Configs.Wallets
{
    public class WalletTransactionConfigs : IEntityTypeConfiguration<WalletTransaction>
    {
        public void Configure(EntityTypeBuilder<WalletTransaction> builder)
        {
            builder.ToTable("WalletTransactions");
            builder.HasKey(a => a.Id);
            builder.Property(a => a.WalletId).IsRequired();
            builder.Property(a => a.TrxAmount).IsRequired();
            builder.Property(a => a.TrxType).IsRequired();
            builder.Property(a => a.Active);
            builder.Property(a => a.FromUserId).IsRequired();
            builder.Property(a => a.TrxDate).IsCreateDate().IsRequired();
            builder.HasOne(a => a.Wallet).WithMany(x => x.Transactions).OnDelete(DeleteBehavior.Restrict).HasForeignKey(x => x.WalletId);
            builder.HasOne(a => a.FromUser).WithMany(x => x.Transactions).OnDelete(DeleteBehavior.Restrict).HasForeignKey(x => x.FromUserId);
        }
    }
}
