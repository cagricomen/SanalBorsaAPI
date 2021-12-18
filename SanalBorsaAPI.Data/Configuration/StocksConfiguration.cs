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
    public class StocksConfiguration : IEntityTypeConfiguration<Stocks>
    {
        public void Configure(EntityTypeBuilder<Stocks> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).UseIdentityColumn();
            builder.Property(x => x.Name).IsRequired();
            builder.Property(x => x.LastPrice).HasColumnType("decimal(20,2)");
            builder.Property(x => x.HighestPrice).HasColumnType("decimal(20,2)");
            builder.Property(x => x.LowestPrice).HasColumnType("decimal(20,2)");
        }
    }
    public class StocksLogsConfiguration : IEntityTypeConfiguration<StocksLogs>
    {
        public void Configure(EntityTypeBuilder<StocksLogs> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).UseIdentityColumn();
        }
    }
}
