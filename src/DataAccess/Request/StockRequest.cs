using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Request
{
    public class StockRequest
    {
        public int StockId { get; set; }
        public string ItemName { get; set; }
        public string StockDesc { get; set; }
        public string SerialNo { get; set; }
        public decimal StockValue { get; set; }
        public int StockQty { get; set; }
        public string ItemStatus { get; set; }
        public string StockedBy { get; set; }
        public DateTime StockedOn { get; set; }
    }
}
