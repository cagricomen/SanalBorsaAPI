using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SanalBorsaAPI.Core.PageData
{
    public class ReturnPagedData<T>
    {
        public List<T> Items { get; set; }
        public int PageCount { get; set; }
        public int ItemCount { get; set; }
        public int CurrentPage { get; set; }
    }
}
