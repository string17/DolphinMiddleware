using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Request
{
    public class IncidentLogRequest
    {
        public int IncidentId { get; set; }
        public string TerminalNo { get; set; }
        public string IncidentTitle { get; set; }
        public string IncidentDesc { get; set; }
        public string IncidentPriority { get; set; }
        public string LoggedBy { get; set; }
        public DateTime LoggedOn { get; set; }
    }


    public class TreatRequest
    {
        public int IncidentId { get; set; }
        public string TerminalNo { get; set; }
        public string TreatedBy { get; set; }
        public string TestDiagnosed { get; set; }
        public string EngineerAssigned { get; set; }
        public string LoggedBy { get; set; }
        public DateTime TreatedOn { get; set; }
    }


    public class CloseRequest
    {
        public int IncidentId { get; set; }
        public string TerminalNo { get; set; }
        public List<string> PartReplaced { get; set; }
        public bool IsParReplaced { get; set; }
        public string ResolvedBy { get; set; }
        public DateTime ResolvedOn { get; set; }
        public bool IsCallResolved { get; set; }
        public string ClosedRemark { get; set; }
    }
}
