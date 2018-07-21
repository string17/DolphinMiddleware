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
    public class IncidentManagement
    {
        private readonly DolphinDb _db = DolphinDb.GetInstance();
        private static readonly ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        private readonly TerminalManagement _terminal = new TerminalManagement();
        //private readonly AuditManagement _audit = new AuditManagement();


        public List<IncidentDetailsObj> GetAllPendingCalls()
        {
            return _db.Fetch<IncidentDetailsObj>("select * from Dol_Incident where IsCallResolved=@0", false).ToList();
        }


        public IncidentDetailsObj GetIncidentDetails(int IncidentNo)
        {
            //string IncidentNo = GenerateIncidentNo(IncidentId);
            return _db.FirstOrDefault<IncidentDetailsObj>("select * from Dol_Incident where IncidentId=@0", IncidentNo);
        }


        public bool InsertIncident(IncidentLogRequest request, out int IncidentId)
        {
            try
            {
                IncidentId = 0;
                var param = new DolIncident();
                param.Terminalno = request.TerminalNo;
                param.Incidenttitle = request.IncidentTitle;
                param.Incidentdesc = request.IncidentDesc;
                param.Incidentpriority = request.IncidentPriority;
                param.Loggedby = request.LoggedBy;
                param.Loggedon = DateTime.Now;
                _db.Insert(param);
                IncidentId = Convert.ToInt32(param.Incidentid);
                return true;
            }
            catch (Exception ex)
            {
                log.Fatal("ClassName:IncidentManagement MethodName: InsertIncident", ex);
                IncidentId = 0;
                return false;
            }
        }


        public bool UpdateIncident(IncidentLogRequest request)
        {
            try
            {
                return true;
            }
            catch (Exception ex)
            {
                log.Fatal("ClassName:IncidentManagement MethodName: UpdateIncident", ex);
                return false;
            }
        }

        public bool CloseIncident(CloseRequest request)
        {
            try
            {
                var param = _db.FirstOrDefault<DolIncident>("where IncidentId=@0", request.IncidentId);
                param.Resolvedby = request.ResolvedBy;
                param.Resolvedon = request.ResolvedOn;
                param.Ispartreplaced = request.IsParReplaced;
                param.Pegrade = ""; //Generate Performance grade based on SLA
                param.Iscallresolved = request.IsCallResolved;
                param.Closedremark = request.ClosedRemark;
                _db.Update(param);
                return true;
            }
            catch (Exception ex)
            {
                log.Fatal("ClassName:IncidentManagement MethodName: CloseIncident", ex);
                return false;
            }
        }

        public string GenerateIncidentNo(int IncidentId)
        {
            var IncidentNo = IncidentId.ToString().PadLeft(7, '0');
            return IncidentNo;
        }

        public IncidentLogResponse LogNewRequest(IncidentLogRequest request)
        {
            if (request.TerminalNo == string.Empty)
            {
                return new IncidentLogResponse
                {
                    ResponseCode = "01",
                    ResponseMessage = "Kindly supply terminal Number",
                    IncidentNo = "XX"
                };
            }

            var supportStatus = _terminal.GetTerminalDetailsByNo(request.TerminalNo);
            if (supportStatus.IsUnderSupport == false)
            {
                return new IncidentLogResponse
                {
                    ResponseCode = "02",
                    ResponseMessage = "terminal is not support by this vendor",
                    IncidentNo = "XX"
                };
            }
            int IncidentId;
            bool newRequest = InsertIncident(request, out IncidentId);
            if (!newRequest)
            {
                return new IncidentLogResponse
                {
                    ResponseCode = "03",
                    ResponseMessage = "Unable to log this request",
                    IncidentNo = "XX"
                };
            }
            else
            {
                return new IncidentLogResponse
                {
                    ResponseCode = "00",
                    ResponseMessage = "Request has been successfully logged",
                    IncidentNo = GenerateIncidentNo(IncidentId)
                };
            }
        }


        public IncidentDetailsResponse AllPendingRequest()
        {
            var result = GetAllPendingCalls();
            if (result == null)
            {
                return new IncidentDetailsResponse
                {
                    ResponseCode = "01",
                    ResponseMessage = "No data found",
                     IncidentDetails = new List<IncidentDetailsObj>()
                };
            }
            else
            {
                return new IncidentDetailsResponse
                {
                    ResponseCode = "00",
                    ResponseMessage = "Success",
                    IncidentDetails = result
                };
            }
        }
    }
}
