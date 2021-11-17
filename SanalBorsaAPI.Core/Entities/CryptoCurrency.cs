using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SanalBorsaAPI.Core.Entities
{
    public class CryptoCurrency
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal BuyPriceDolar { get; set; }
        public decimal BuyPriceTL { get; set; }
        public string MarketingValue { get; set; }
        public string MarketingSize { get; set; }
        public decimal Change { get; set; }
        public DateTime UpdateTime { get; set; }
        public string ImgPath { get; set; }

    }
}
