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
        public float BuyPrice { get; set; }
        public float SalePrice { get; set; }
        public decimal Change { get; set; }
        public DateTime UpdateTime { get; set; }
    }
}
