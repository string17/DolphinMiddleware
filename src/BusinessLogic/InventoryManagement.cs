using DataAccess.Request;
using DataAccess.Response;
using DolphinContext.Data.Models;
using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic
{
    public class InventoryManagement
    {
        private readonly DolphinDb _db = DolphinDb.GetInstance();
        private static readonly ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        private readonly AuditManagement _audit = new AuditManagement();
        private readonly BrandManagement _brand = new BrandManagement();
        private readonly ClientManagement _client = new ClientManagement();
        private readonly UserManagement _user = new UserManagement();



        public List<StockDetailsObj> GetAllStocks()
        {
            return _db.Fetch<StockDetailsObj>("select * from Dol_Stocked_Part").ToList();
        }


        public StockDetailsObj GetStockDetailsById(int StockId)
        {
            return _db.FirstOrDefault<StockDetailsObj>("select * from Dol_Stocked_Part where StockId=@0", StockId);
        }

        public bool InsertStocks(StockRequest param)
        {
            try
            {
                var request = new DolStockedPart();
                request.Itemname = param.ItemName;
                request.Itemstatus = param.ItemStatus;
                request.Serialno = param.SerialNo;
                request.Stockdesc = param.StockDesc;
                request.Stockedby = param.StockedBy;
                request.Stockedon = param.StockedOn;
                request.Stockqty = param.StockQty;
                _db.Insert(request);
                return true;
            }
            catch(Exception ex)
            {
                log.Fatal("ClassName:InventoryManagement MethodName: InsertStock", ex);
                return false;
            }
        }


        public bool UpdateStocks(StockRequest param)
        {
            try
            {
                var request = _db.FirstOrDefault<DolStockedPart>("where StockId=@0",param.StockId);
                request.Itemname = param.ItemName;
                request.Itemstatus = param.ItemStatus;
                request.Serialno = param.SerialNo;
                request.Stockdesc = param.StockDesc;
                request.Stockedby = param.StockedBy;
                request.Stockedon = param.StockedOn;
                request.Stockqty = param.StockQty;
                _db.Insert(request);
                return true;
            }
            catch (Exception ex)
            {
                log.Fatal("ClassName:InventoryManagement MethodName: UpdateStock", ex);
                return false;
            }
        }
    }
}
