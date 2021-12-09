using HtmlAgilityPack;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Quartz;
using SanalBorsaAPI.Core.Entities;
using SanalBorsaAPI.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SanalBorsaAPI.Service.Services
{
    public class QuartzCrud : IJob
    {
        public IServiceProvider Services { get; }
        private readonly ILogger<QuartzCrud> _logger;
        public QuartzCrud(IServiceProvider services, ILogger<QuartzCrud> logger)
        {
            _logger = logger;
            Services = services;
        }

        public Task Execute(IJobExecutionContext context)
        {
            using (var scope = Services.CreateScope())
            {
                var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
                var goldens = new List<Golden>();
                var goldenAdress = "https://altin.doviz.com/";
                var hweb = new HtmlAgilityPack.HtmlWeb();
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
                    
                }
                foreach (var item in goldens)
                {
                    var result =  db.Goldens.FirstOrDefault(x => x.Name == item.Name);
                    if (result != null)
                    {
                        result.SalePrice = item.SalePrice;
                        result.BuyPrice = item.BuyPrice;
                        result.Change = item.Change;
                        result.UpdateTime = item.UpdateTime;
                    }
                    else
                    {
                        db.Goldens.Add(item);
                    }
                    _logger.LogInformation($"Eklenen eleman : {item.Name} ");
                }
                 db.SaveChanges();
            }
            return Task.CompletedTask;
        }
    }
}
