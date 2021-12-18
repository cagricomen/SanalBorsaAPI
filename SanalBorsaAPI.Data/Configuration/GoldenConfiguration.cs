using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SanalBorsaAPI.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SanalBorsaAPI.Data.Configuration
{
    public class GoldenConfiguration : IEntityTypeConfiguration<Golden>
    {
        public void Configure(EntityTypeBuilder<Golden> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).UseIdentityColumn();
            builder.Property(x => x.Name).IsRequired();
            builder.Property(x => x.BuyPrice).HasColumnType("decimal(20,4)");
            builder.Property(x => x.SalePrice).HasColumnType("decimal(20,4)");
            builder.Property(x => x.Change).HasColumnType("decimal(20,2)");

        }
    }
    public class GoldenLogsConfiguration : IEntityTypeConfiguration<GoldenLogs>
    {
        public void Configure(EntityTypeBuilder<GoldenLogs> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).UseIdentityColumn();
        }
    }
}
