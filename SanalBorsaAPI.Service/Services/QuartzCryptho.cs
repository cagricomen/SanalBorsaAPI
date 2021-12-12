using HtmlAgilityPack;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Quartz;
using SanalBorsaAPI.Core.Entities;
using SanalBorsaAPI.Core.Repositories;
using SanalBorsaAPI.Core.Services;
using SanalBorsaAPI.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SanalBorsaAPI.Service.Services
{
    public class QuartzCryptho : IJob
    {
        private readonly IRepository<CryptoCurrency> db;
        public IServiceProvider Services { get; }
        private readonly ILogger<QuartzCryptho> _logger;
        public QuartzCryptho(IServiceProvider services, ILogger<QuartzCryptho> logger, IRepository<CryptoCurrency> _db )
        {
            db = _db;
            _logger = logger;
            Services = services;
        }

        public async  Task Execute(IJobExecutionContext context)
        {
            // Golden();
            var cryptoList = new List<CryptoCurrency>();
            var cryptoAdress = "https://www.doviz.com/kripto-paralar";
            var hweb = new HtmlWeb();
            HtmlDocument htmlDocument = hweb.Load(cryptoAdress);
            var tBody = htmlDocument.DocumentNode.SelectSingleNode("//*[@id='coins']/tbody");
            if (tBody != null)
            {
                var tableRows = tBody.Elements("tr");
                foreach (var row in tableRows)
                {
                    var crpyto = new CryptoCurrency();
                    var tdElement = row.Elements("td");
                    int i = 0;
                    foreach (var tdData in tdElement)
                    {
                        if (i == 0)
                        {
                            var aElement = tdData.Element("a");
                            var sName = aElement.GetDirectInnerText().Trim();
                            crpyto.Name = sName;
                            var imgElement = aElement.Element("img");
                            HtmlAttribute src = imgElement.Attributes["src"];
                            crpyto.ImgPath = src.Value;

                        }
                        if (i == 1)
                        {
                            var sName = tdData.GetDirectInnerText().Trim();
                            var changee = sName.Substring(1, sName.Length - 1);
                            crpyto.BuyPriceDolar = Decimal.Parse(changee);
                        }
                        if (i == 2)
                        {
                            var sName = tdData.GetDirectInnerText().Trim();
                            var changee = sName.Substring(1, sName.Length - 1);
                            crpyto.BuyPriceTL = Decimal.Parse(changee);
                        }
                        if (i == 3)
                        {
                            var marketValue = tdData.GetDirectInnerText().Trim().Substring(1).Replace('.', ' ').Replace(" ", "");
                            crpyto.MarketingValue = marketValue;
                        }
                        if (i == 4)
                        {
                            crpyto.MarketingSize = tdData.GetDirectInnerText().Trim();
                        }
                        if (i == 5)
                        {
                            var sName = tdData.GetDirectInnerText().Trim();
                            var changee = sName.Substring(1, sName.Length - 1);
                            crpyto.Change = decimal.Parse(changee);
                        }
                        if (i == 6)
                        {
                            crpyto.UpdateTime = DateTime.Now;
                        }
                        i++;
                    }
                    cryptoList.Add(crpyto);

                }
            }
            foreach (var item in cryptoList)
            {
                var result = await db.SingleOrDefaultAsync(x => x.Name == item.Name);
                if (result != null && !result.Name.Equals("-"))
                {
                    result.Name = item.Name;
                    result.BuyPriceDolar = item.BuyPriceDolar;
                    result.BuyPriceTL = item.BuyPriceTL;
                    result.MarketingSize = item.MarketingSize;
                    result.MarketingValue = item.MarketingValue;
                    result.Change = item.Change;
                    result.UpdateTime = item.UpdateTime;
                    result.ImgPath = item.ImgPath;
                    db.Update(item);
                    _logger.LogInformation($"Güncellenen Crypto elemanı : {item.Name} ");
                }
                else
                {
                    if (!item.Name.Equals("-"))
                    {
                        await db.AddAsync(item);
                        _logger.LogInformation($"Eklenen Crypto elemanı : {item.Name} ");
                    }
                }
            }
            
            
                
        }
    }
}
