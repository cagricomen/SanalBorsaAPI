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
    public class QuartzExChange : IJob
    {
        private readonly IRepository<ExChangeRates> db;
        public IServiceProvider Services { get; }
        private readonly ILogger<QuartzExChange> _logger;

        public QuartzExChange(IRepository<ExChangeRates> _db, IServiceProvider services, ILogger<QuartzExChange> logger)
        {
            db = _db;
            Services = services;
            _logger = logger;
        }

        public async Task Execute(IJobExecutionContext context)
        {
            var ratesList = new List<ExChangeRates>();
            var ratesAdress = "https://kur.doviz.com/";
            var hweb = new HtmlWeb();
            HtmlDocument htmlDocument = hweb.Load(ratesAdress);
            var tBody = htmlDocument.DocumentNode.SelectSingleNode("//*[@id='currencies']/tbody");
            if (tBody != null)
            {
                var tableRows = tBody.Elements("tr");
                foreach (var row in tableRows)
                {
                    if (row.Attributes.Contains("style"))
                    {
                        continue;
                    }
                    var changeRates = new ExChangeRates();
                    var tdElement = row.Elements("td");
                    int i = 0;
                    foreach (var tdData in tdElement)
                    {
                        if (i == 0)
                        {
                            var aElement = tdData.Element("a");
                            var sName = aElement.GetDirectInnerText().Trim();
                            changeRates.ShortName = sName.Substring(0, 2).ToLower();
                            changeRates.Name = sName.Substring(5).Trim();
                        }
                        if (i == 1)
                        {
                            changeRates.BuyPrice = Decimal.Parse(tdData.GetDirectInnerText());
                        }
                        if (i == 2)
                        {
                            changeRates.SalePrice = Decimal.Parse(tdData.GetDirectInnerText());
                        }
                        if (i == 3)
                        {
                            changeRates.HighestPrice = Decimal.Parse(tdData.GetDirectInnerText());
                        }
                        if (i == 4)
                        {
                            changeRates.LowestPrice = Decimal.Parse(tdData.GetDirectInnerText());
                        }
                        if (i == 5)
                        {
                            var sName = tdData.GetDirectInnerText().Trim();
                            var changee = sName.Substring(1, sName.Length - 1);
                            changeRates.Change = Decimal.Parse(changee);
                        }
                        if (i == 6)
                        {
                            changeRates.UpdateTime = DateTime.Now;
                        }
                        i++;
                    }
                    ratesList.Add(changeRates);


                }
            }
            foreach (var item in ratesList)
            {
                var result = await db.SingleOrDefaultAsync(x => x.Name == item.Name);
                if(result != null)
                {
                    result.BuyPrice = item.BuyPrice;
                    result.SalePrice = item.SalePrice;
                    result.HighestPrice = item.HighestPrice;
                    result.LowestPrice = item.LowestPrice;
                    result.Change = item.Change;
                    result.UpdateTime = item.UpdateTime;
                    _logger.LogInformation($"Güncellenen ExChange elemanı  : {item.Name} ");
                }
                else
                {
                    await db.AddAsync(item);
                    _logger.LogInformation($"Eklenen ExChange elemanı  : {item.Name} ");
                }
            }
        }
    }
}
