using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StudentHub.Models.Network;
using StudentHub.Repositories.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentHub.Repositories.Configs.Network
{
    public class TheHubConfigs  : IEntityTypeConfiguration<TheHub>
    {
        public void Configure(EntityTypeBuilder<TheHub> builder)
        {
            builder.ToTable("Hubs");
            builder.HasKey(a => a.Id);        
            builder.Property(a => a.DistrictId).IsRequired(); 
            builder.Property(a => a.Name).IsRequired();  
            builder.Property(a => a.CreateDate).IsCreateDate().IsRequired();
            builder.HasOne(a => a.District).WithMany(x => x.Hubs).OnDelete(DeleteBehavior.Restrict).HasForeignKey(x => x.DistrictId);
        }
    }
}
