using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SanalBorsaAPI.Core.Entities
{
    public class Golden
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal BuyPrice { get; set; }
        public decimal SalePrice { get; set; }
        public decimal Change { get; set; }
        public DateTime UpdateTime { get; set; }
    }
}
