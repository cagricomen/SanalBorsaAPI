using HtmlAgilityPack;
using Microsoft.Extensions.Logging;
using Quartz;
using SanalBorsaAPI.Core.Entities;
using SanalBorsaAPI.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SanalBorsaAPI.Service.Services
{
    public class QuartzGolden : IJob
    {
        private readonly IRepository<Golden> db;
        private readonly IRepository<GoldenLogs> logRepo;
        public IServiceProvider Services { get; }
        private readonly ILogger<QuartzGolden> _logger;

        public QuartzGolden(IRepository<Golden> _db, IServiceProvider services, ILogger<QuartzGolden> logger, IRepository<GoldenLogs> _logRepo)
        {
            logRepo = _logRepo;
            db = _db;
            Services = services;
            _logger = logger;
        }

        public async Task Execute(IJobExecutionContext context)
        {
            var goldens = new List<Golden>();
            var goldenAdress = "https://altin.doviz.com/";
            var hweb = new HtmlWeb();
            HtmlDocument htmlDocument = hweb.Load(goldenAdress);
            var tBody = htmlDocument.DocumentNode.SelectSingleNode("//*[@id='golds']");
            if (tBody != null)
            {
                var tableRows = tBody.Elements("tr");
                foreach (var row in tableRows)
                {
                    if (row.Attributes.Contains("style"))
                    {
                        continue;
                    }
                    var golden = new Golden();
                    var tdElement = row.Elements("td");
                    int i = 0;
                    foreach (var tdData in tdElement)
                    {
                        if (i == 0)
                        {
                            var aElement = tdData.Element("a");
                            var sName = aElement.GetDirectInnerText().Trim();
                            if (sName.Equals("Ons Altın"))
                            {
                                golden.Name = $"{sName}($)";
                            }
                            else golden.Name = sName;
                        }
                        if (i == 1)
                        {
                            var sss = tdData.GetDirectInnerText().Trim();
                            if (sss.Contains('$'))
                            {
                                golden.BuyPrice = Decimal.Parse(sss.Substring(1));
                            }
                            else golden.BuyPrice = Decimal.Parse(sss);
                        }
                        if (i == 2)
                        {
                            var sss = tdData.GetDirectInnerText().Trim();
                            if (sss.Contains('$'))
                            {
                                golden.SalePrice = Decimal.Parse(sss.Substring(1));
                            }
                            else golden.SalePrice = Decimal.Parse(sss);
                        }
                        if (i == 3)
                        {
                            var sName = tdData.GetDirectInnerText().Trim();
                            var changee = sName.Substring(1);
                            golden.Change = Decimal.Parse(changee);
                        }
                        if (i == 4)
                        {
                            golden.UpdateTime = DateTime.Now;
                        }
                        i++;
                    }
                    goldens.Add(golden);

                }
                foreach (var item in goldens)
                {
                    var result = await db.SingleOrDefaultAsync(x => x.Name == item.Name);
                    if (result != null)
                    {
                        result.Name = item.Name;
                        result.SalePrice = item.SalePrice;
                        result.BuyPrice = item.BuyPrice;
                        result.Change = item.Change;
                        result.UpdateTime = item.UpdateTime;
                        db.Update(item);
                        _logger.LogInformation($"Updated Golden elemanı  : {item.Name} ");
                        var goldLog = new GoldenLogs
                        {
                            CategoryName = "Golden",
                            GoldenId = result.Id,
                            UpdateTime = DateTime.Now,
                            Log = $"Updated Golden element  : {item.Name} "

                        };
                        await logRepo.AddAsync(goldLog);
                    }
                    else
                    {
                        await db.AddAsync(item);
                        _logger.LogInformation($"Added Golden element : {item.Name} ");
                        var goldLog = new GoldenLogs
                        {
                            CategoryName = "Golden",
                            GoldenId = item.Id,
                            UpdateTime = DateTime.Now,
                            Log = $"Added Golden element  : {item.Name} "

                        };
                        await logRepo.AddAsync(goldLog);
                    }

                }
            }
        }
    }
}
