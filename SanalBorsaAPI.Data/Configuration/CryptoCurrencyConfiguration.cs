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
    public class CryptoCurrencyConfiguration : IEntityTypeConfiguration<CryptoCurrency>
    {
        public void Configure(EntityTypeBuilder<CryptoCurrency> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).UseIdentityColumn();
            builder.Property(x => x.Name).IsRequired();
        }
    }
    public class CryptoCurrencyLogsConfiguration : IEntityTypeConfiguration<CryptoCurrencyLogs>
    {
        public void Configure(EntityTypeBuilder<CryptoCurrencyLogs> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).UseIdentityColumn();
        }
    }
}
