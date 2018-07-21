using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Response
{
    public class StockResponse
    {
        public string ResponseCode { get; set; }
        public string ResponseMessage { get; set; }
        public List<StateDetailsObj> StockDetails { get; set; }
    }


    public class StockDetailsObj
    {
            public int StockId { get; set; }
            public string ItemName { get; set; }
            public string StockDesc { get; set; }
            public string SerialNo { get; set; }
            public decimal ValueRate { get; set; }
            public int StockQty { get; set; }
            public string ItemStatus { get; set; }
            public string StockedBy { get; set; }
            public DateTime StockedOn { get; set; }
    }
}
