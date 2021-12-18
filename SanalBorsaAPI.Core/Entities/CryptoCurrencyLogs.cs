using System;

namespace SanalBorsaAPI.Core.Entities
{
    public class CryptoCurrencyLogs
    {
        public int Id { get; set; }
        public int CrypthoId { get; set; }
        public string CategoryName { get; set; }
        public DateTime UpdateTime { get; set; }
        public string Log { get; set; }
    }
}
