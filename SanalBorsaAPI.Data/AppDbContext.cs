using Microsoft.EntityFrameworkCore;
using SanalBorsaAPI.Core.Entities;
using SanalBorsaAPI.Data.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SanalBorsaAPI.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }
        public DbSet<Golden> Goldens { get; set; }
        public DbSet<CryptoCurrency> CryptoCurrencies { get; set; }
        public DbSet<ExChangeRates> ExChangeRates { get; set; }
        public DbSet<Stocks> Stocks { get; set; }
        public DbSet<GoldenLogs> GoldenLogs { get; set; }
        public DbSet<CryptoCurrencyLogs> CryptoCurrencyLogs { get; set; }
        public DbSet<ExChangeRatesLogs> ExChangeRatesLogs { get; set; }
        public DbSet<StocksLogs> StocksLogs { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new GoldenConfiguration());
            modelBuilder.ApplyConfiguration(new CryptoCurrencyConfiguration());
            modelBuilder.ApplyConfiguration(new ExChangeRatesConfiguration());
            modelBuilder.ApplyConfiguration(new StocksConfiguration());
            modelBuilder.ApplyConfiguration(new StocksLogsConfiguration());
            modelBuilder.ApplyConfiguration(new GoldenLogsConfiguration());
            modelBuilder.ApplyConfiguration(new ExChangeRatesLogsConfiguration());
            modelBuilder.ApplyConfiguration(new CryptoCurrencyLogsConfiguration());
        }
    }
}
