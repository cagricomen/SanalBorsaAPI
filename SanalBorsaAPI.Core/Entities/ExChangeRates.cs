using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SanalBorsaAPI.Core.Entities
{
    public class ExChangeRates
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ShortName { get; set; }

        public decimal BuyPrice { get; set; }
        public decimal SalePrice { get; set; }
        public decimal HighestPrice { get; set; }
        public decimal LowestPrice { get; set; }
        public decimal Change { get; set; }
        public DateTime UpdateTime { get; set; }
    }
}
