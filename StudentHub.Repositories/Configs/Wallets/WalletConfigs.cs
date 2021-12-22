using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StudentHub.Models.Wallets;
using StudentHub.Repositories.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentHub.Repositories.Configs.Wallets
{
    public class WalletConfigs : IEntityTypeConfiguration<Wallet>
    {
        public void Configure(EntityTypeBuilder<Wallet> builder)
        {
            builder.ToTable("Wallets");
            builder.HasKey(a => a.Id);
            builder.Property(a => a.UserId).IsRequired();
            builder.Property(a => a.Active);
            builder.Property(a => a.CreateDate).IsCreateDate().IsRequired();
            builder.HasOne(a => a.User).WithMany(x => x.Wallets).OnDelete(DeleteBehavior.Restrict).HasForeignKey(x => x.UserId);
        }
    }
}
