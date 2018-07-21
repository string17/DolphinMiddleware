using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Response
{
    public class IncidentLogResponse
    {
            public string ResponseCode { get; set; }
            public string ResponseMessage { get; set; }
            public string IncidentNo { get; set; }
        }


        public class IncidentDetailsResponse
        {
            public string ResponseCode { get; set; }
            public string ResponseMessage { get; set; }
            public List<IncidentDetailsObj> IncidentDetails { get; set; }
        }

        public class IncidentDetailsObj
        {
        public string IncidentNo { get; set; }
        public string TerminalNo { get; set; }
        public string TerminalRef { get; set; }
        public string Engineer { get; set; }
        public int StateId { get; set; }
        public int ClientId { get; set; }
        public string ClientAlias { get; set; }
        public string Location { get; set; }
        public string LoggedBy { get; set; }
        public string AgentId { get; set; }
        public DateTime TreatedOn { get; set; }
        public DateTime ResolovedOn { get; set; }
        public string EngineerAssigned { get; set; }
        public string ResolvedBy { get; set; }
        public DateTime LoggedOn { get; set; }
        public bool IsCallResolved { get; set; }
        public string PEGrade { get; set; }
    }
    }
