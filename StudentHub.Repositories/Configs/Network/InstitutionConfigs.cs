using System;
using System.Collections.Generic;
using System.Globalization; 
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StudentHub.Models.Auth;
using StudentHub.Models.Network;
using StudentHub.Repositories.Extensions;

namespace StudentHub.Repositories.Configs
{
    public class InstitutionConfigs : IEntityTypeConfiguration<Institution>
    {
        public void Configure(EntityTypeBuilder<Institution> builder)
        {
            builder.ToTable("Institutions");
            builder.HasKey(a => a.Id);
            builder.Property(a => a.HubId).IsRequired();
            builder.Property(a => a.ManagementName);
            builder.Property(a => a.ManagementType);
            builder.Property(a => a.EIIN).IsRequired();            
            builder.Property(a => a.InstitutionType).IsRequired();            
            builder.Property(a => a.Name).IsRequired();
            builder.Property(a => a.CreateDate).IsCreateDate().IsRequired();       
            builder.HasOne(a => a.Hub).WithMany(x=>x.Institutions).HasForeignKey(x=>x.HubId);      
           
        }
    }
}