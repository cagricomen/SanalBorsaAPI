using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SanalBorsaAPI.Core.Entities
{
    public class Stocks
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ShortName { get; set; }
        public float LastPrice { get; set; }
        public float HighestPrice { get; set; }
        public float LowestPrice { get; set; }
        public string MarketingSize { get; set; }
        public decimal Change { get; set; }
        public DateTime UpdateTime { get; set; }

    }
}
