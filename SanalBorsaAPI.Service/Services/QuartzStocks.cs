using HtmlAgilityPack;
using Microsoft.Extensions.Logging;
using Quartz;
using SanalBorsaAPI.Core.Entities;
using SanalBorsaAPI.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SanalBorsaAPI.Service.Services
{
    public class QuartzStocks : IJob
    {
        private readonly IRepository<Stocks> db;
        public IServiceProvider Services { get; }
        private readonly ILogger<QuartzStocks> _logger;

        public QuartzStocks(IRepository<Stocks> _db, IServiceProvider services, ILogger<QuartzStocks> logger)
        {
            db = _db;
            Services = services;
            _logger = logger;
        }

        public async Task Execute(IJobExecutionContext context)
        {
            var stocksList = new List<Stocks>();
            var stockAdress = "https://borsa.doviz.com/hisseler";
            var hweb = new HtmlWeb();
            HtmlDocument htmlDocument = hweb.Load(stockAdress);
            var tBody = htmlDocument.DocumentNode.SelectSingleNode("//*[@id='stocks']");
            if(tBody != null)
            {
                var tableRows = tBody.Elements("tr");
                foreach (var rows in tableRows)
                {
                    var stock = new Stocks();
                    int i = 0;
                    var tdElement = rows.Elements("td");
                    foreach (var tdData in tdElement)
                    {
                        if (i == 0)
                        {
                            var aElement = tdData.Element("a");
                            var sName = aElement.GetDirectInnerText().Trim();
                            stock.ShortName = sName.Split('-').First();
                            stock.Name = sName.Split('-').Last();
                        }
                        if (i == 1)
                        {
                            stock.LastPrice = decimal.Parse(tdData.GetDirectInnerText().Trim());
                        }
                        if (i == 2)
                        {
                            stock.HighestPrice = decimal.Parse(tdData.GetDirectInnerText().Trim());
                        }
                        if (i == 3)
                        {
                            stock.LowestPrice = decimal.Parse(tdData.GetDirectInnerText().Trim());
                        }
                        if (i == 4)
                        {
                            string deneme = tdData.GetDirectInnerText().Replace('.', '!');
                            deneme = deneme.Replace(',', '.');
                            deneme = deneme.Replace('!', ' ');
                            deneme = deneme.Replace(" ", "");
                            stock.MarketingSize = deneme;
                        }
                        if (i == 5)
                        {
                            stock.Change = decimal.Parse(tdData.GetDirectInnerText().Trim().Substring(1));
                        }
                        if (i == 6)
                        {
                            stock.UpdateTime = DateTime.Now;
                        }
                        i++;
                    }
                    stocksList.Add(stock);
                }
            }
            foreach (var item in stocksList)
            {
                var result = await db.SingleOrDefaultAsync(x => x.Name == item.Name);
                if(result != null)
                {
                    result.LastPrice = item.LastPrice;
                    result.HighestPrice = item.HighestPrice;
                    result.LowestPrice = item.LowestPrice;
                    result.MarketingSize = item.MarketingSize;
                    result.Change = item.Change;
                    result.UpdateTime = item.UpdateTime;
                    db.Update(item);
                    _logger.LogInformation($"Güncellenen Stocks elemanı  : {item.Name} ");
                }
                else
                {
                    await db.AddAsync(item);
                    _logger.LogInformation($"Eklenen Stocks elemanı  : {item.Name} ");
                }
            }
        }
    }
}
