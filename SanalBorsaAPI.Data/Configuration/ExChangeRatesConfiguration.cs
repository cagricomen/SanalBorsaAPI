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
    public class ExChangeRatesConfiguration : IEntityTypeConfiguration<ExChangeRates>
    {
        public void Configure(EntityTypeBuilder<ExChangeRates> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).UseIdentityColumn();
            builder.Property(x => x.Name).IsRequired();
            builder.Property(x => x.BuyPrice).HasColumnType("decimal(20,4)");
            builder.Property(x => x.SalePrice).HasColumnType("decimal(20,4)");
            builder.Property(x => x.HighestPrice).HasColumnType("decimal(20,4)");
            builder.Property(x => x.LowestPrice).HasColumnType("decimal(20,4)");
        }
    }
    public class ExChangeRatesLogsConfiguration : IEntityTypeConfiguration<ExChangeRatesLogs>
    {
        public void Configure(EntityTypeBuilder<ExChangeRatesLogs> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).UseIdentityColumn();
        }
    }
}
