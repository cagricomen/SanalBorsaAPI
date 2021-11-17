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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new GoldenConfiguration());
            modelBuilder.ApplyConfiguration(new CryptoCurrencyConfiguration());
            modelBuilder.ApplyConfiguration(new ExChangeRatesConfiguration());
            modelBuilder.ApplyConfiguration(new StocksConfiguration());
        }
    }
}
