using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SanalBorsaAPI.Core.Entities
{
    public class GoldenLogs
    {
        public int Id { get; set; }
        public string CategoryName { get; set; }
        public int GoldenId { get; set; }
        public DateTime UpdateTime { get; set; }
        public string Log { get; set; }
    }
}
