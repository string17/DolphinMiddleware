using DataAccess.CustomObjects;
using DataAccess.Request;
using DataAccess.Response;
using DolphinContext.Data.Models;
using log4net;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace BusinessLogic
{
    public class AuditManagement
    {
        private readonly DolphinDb _db = DolphinDb.GetInstance();
        private static readonly ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        private readonly UserManagement _user = new UserManagement();


        public bool InsertAudit(string UserName,string UserActivity,string Comment, DateTime EventDate,string SystemName,string SystemIp)
        {
            try
            {
                var audit = new AuditTrail();
                audit.Username = UserName;
                audit.Useractivity = UserActivity;
                audit.Comment = Comment;
                audit.Eventdate = EventDate;
                audit.Systemname = SystemName;
                audit.Systemip = SystemIp;
                _db.Insert(audit);
                return true;
            }
            catch(Exception ex)
            {
                log.ErrorFormat("InsertAudit", ex.Message);
                return false;
            }
           
        }

        public List<AuditDetailsObj> GetAuditById()
        {
              return _db.Fetch<AuditDetailsObj>("select * from Audit_Trail order by AuditId desc");
        }


        public bool InsertSessionTracker(string UserName, string SystemIp, string SystemName)
        {
            try
            {
                int SessionId = GetLoginSequence();
                var track = new UserTracking();
                track.Username = UserName;
                track.Sessionid = new EncryptionManager().EncryptValue(SessionId.ToString());
                track.Systemip = SystemIp;
                track.Isuserlogoff = false;
                track.Isuserlogout = false;
                track.Systemname = SystemName;
                track.Logindate = DateTime.Now;
                _db.Insert(track);
                return true;
            }
            catch (Exception ex)
            {
                log.ErrorFormat("InsertSession", ex.Message);
                return false;
            }
        }


        public UserTracking TrackLogin(string username)
        {
            return _db.FirstOrDefault<UserTracking>("select * from User_Tracking where UserName =@0", username);
        }


        public int GetLoginSequence()
        {
            return _db.ExecuteScalar<int>("select top 1 Tid from User_Tracking order by Tid desc");
        }


        public bool TerminateSession(string Username)
        {
            string sql = "delete from User_Tracking where UserName =@0";
            var actual = _db.Delete<UserTracking>(sql, Username);
            if(actual !=0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }


        public LoginResponse UserLogout(LoginRequest param)
        {
            bool userLogout = TerminateSession(param.UserName);
            if (userLogout)
            {
                log.InfoFormat(param.Computername, param.SystemIp, param.UserName, Constants.ActionType.LogoutAccount.ToString());
                InsertAudit(param.UserName, Constants.ActionType.LogoutAccount.ToString(), "Logout", DateTime.Now, param.Computername, param.SystemIp);
                return new LoginResponse
                {
                    ResponseCode = "00",
                    ResponseMessage = "Logout successful"

                };
            }
            else
            {
                log.InfoFormat(param.Computername, param.SystemIp, param.UserName, Constants.ActionType.LogoutAccount.ToString());
                return new LoginResponse
                {
                    ResponseCode = "01",
                    ResponseMessage = "Logout failed"
                };
            }
        }


        public bool LogOffUser(LoginRequest request)
        {
            try
            {
                var param = _db.SingleOrDefault<UserTracking>("where UserName=@0 and SystemIp=@1 and IsUserLogOut=@2 and IsUserLogOff=@2", request.UserName, request.SystemIp,false);
                param.Isuserlogoff = true;
                _db.Update(param);
                return true;
            }
            catch(Exception ex)
            {
                log.ErrorFormat("UserLogOff", ex);
                return false;
            }
        }

        public UserDetailsResponse LockScreen(LoginRequest request)
        {
            var result = LogOffUser(request);
            if (!result)
            {
                log.InfoFormat(request.Computername, request.SystemIp, request.UserName, Constants.ActionType.LockAccount.ToString());
                InsertAudit(request.UserName, Constants.ActionType.LogoutAccount.ToString(), "Logoff", DateTime.Now, request.Computername, request.SystemIp);
                return new UserDetailsResponse
                {
                    ResponseCode = "01",
                    ResponseMessage = "Lock not successful",
                    UserDetails = new List<UserDetailsObj>()
                };
            }
            else
            {
                var success = _user.GetUserInfo(request.UserName);
                if (success == null)
                {
                    return new UserDetailsResponse
                    {
                        ResponseCode = "02",
                        ResponseMessage = "Logoff failed"
                    };
                }
                else
                {
                    var userDetails = new List<UserDetailsObj>
                    {
                        success
                    };
                    
                    return new UserDetailsResponse
                    {
                        ResponseCode = "00",
                        ResponseMessage = "Successfully logoff",
                         UserDetails=userDetails
                    };
                }      
            }
        }
    }
}
