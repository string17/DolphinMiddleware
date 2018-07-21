using BusinessLogic;
using DataAccess.Request;
using log4net;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Reflection;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;

namespace DolphinMiddleWare.Controllers
{
    [System.Web.Http.RoutePrefix("dolphin")]
    public class DolphinController : ApiController
    {
        //private static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        private readonly UserManagement _user;
        private readonly MenuManagement _menu;
        private readonly RoleManagement _role;
        private readonly ClientManagement _client;
        private readonly AuditManagement _audit;
        private readonly BrandManagement _brand;
        private readonly TerminalManagement _terminal;
        private readonly IncidentManagement _incident;

        public DolphinController()
        {
            _audit = new AuditManagement();
            _user = new UserManagement();
            _menu = new MenuManagement();
            _role = new RoleManagement();
            _client = new ClientManagement();
            _brand = new BrandManagement();
            _terminal = new TerminalManagement();
            _incident = new IncidentManagement();
        }


        //[System.Web.Http.HttpPost]
        //[System.Web.Http.Route("login")]
        //public IHttpActionResult ValidateUser(object LoginObj)
        //{
        //    var param = JsonConvert.DeserializeObject<LoginRequest>(LoginObj.ToString());
        //    return Request.CreateResponse(new { status = true, result = _user.ValidateUser(param) });
        //}



        // GET: Dolphin
        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("login")]
        public IHttpActionResult ValidateUser(object param)
        {
            var request = JsonConvert.DeserializeObject<LoginRequest>(param.ToString());
            return Json(_user.ValidateUser(request));
        }


        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("lockaccess")]
        public IHttpActionResult LockAccount(object param)
        {
            var request = JsonConvert.DeserializeObject<LoginRequest>(param.ToString());
            var result = _audit.LockScreen(request);
            return Json(result);
        }



        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("userinfo")]
        public IHttpActionResult GetUserInfo(object param)
        {
            //var param = JsonConvert.DeserializeObject<LoginRequest>(LoginObj.ToString());
            return Json(_user.GetUserInfoByUsername(param.ToString()));
        }



        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("changepassword")]
        public IHttpActionResult ResetPassword(object param)
        {
            var request = JsonConvert.DeserializeObject<LoginRequest>(param.ToString());
            var result = _user.ResetPassword(request);
            return Json(result);
        }


        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("Menu")]
        public IHttpActionResult GetUserMenu(object param)
        {
            //var request = JsonConvert.DeserializeObject<LoginRequest>(param.ToString());
            var result = _menu.GetUserMenu(param.ToString());
            return Json(result);
        }



        ////Return all active menu
        ////[System.Web.Http.HttpPost]
        ////[System.Web.Http.Route("Submenu")]
        ////public List<DolMenuItem> GetAllSubMenu(MenuObj menu)
        ////{
        ////    var _menus = _menu.GetAllSubMenu();
        ////    return _menus;
        ////}


        //Return all active Role
        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("allrole")]
        public IHttpActionResult GetAllRole()
        {
            var result = _role.GetAllExistingRole();
            return Json(result);
        }


        //Return all Client details
        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("clientdetails")]
        public IHttpActionResult ClientDetails(object param)
        {
            var request = JsonConvert.DeserializeObject<ClientRequest>(param.ToString());
            var result = _client.GetClientDetailsById(request.ClientId);
            return Json(result);
        }


        //Return all Role details
        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("roledetails")]
        public IHttpActionResult RoleDetails(object param)
        {
            var request = JsonConvert.DeserializeObject<RoleRequest>(param.ToString());
            var result = _role.RoleDetails(request);
            return Json(result);
        }




        //insert new Role
        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("insertrole")]
        public IHttpActionResult InsertRole(object param)
        {
            var request = JsonConvert.DeserializeObject<RoleRequest>(param.ToString());
            var result = _role.SetUpNewRole(request);
            return Json(result);
        }



        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("logout")]
        public IHttpActionResult UserLogout(object param)
        {
            var request = JsonConvert.DeserializeObject<LoginRequest>(param.ToString());
            return Json(_audit.UserLogout(request));
        }



        ////Modify Role
        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("forgotpassword")]
        public IHttpActionResult ForgotPassword(object param)
        {
            var request = JsonConvert.DeserializeObject<LoginRequest>(param.ToString());
            return Json(_user.SendPasswordNotification(request));
        }


        //Modeify Role
        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("modifyrole")]
        public IHttpActionResult ModifyRole(object param)
        {
            var request = JsonConvert.DeserializeObject<RoleRequest>(param.ToString());
            var result = _role.ModifyRoleDetails(request);
            return Json(result);
        }




        ////Delete Role
        //[System.Web.Http.HttpPost]
        //[System.Web.Http.Route("deleterole")]
        //public string DeleteRole(int RoleId)
        //{
        //    int _roles = _role.DeleteRole(RoleId);
        //    if (_roles != 0)
        //    {
        //        return "Deleted";
        //    }
        //    else
        //    {
        //        return "Not Deleted";
        //    }
        //}


        //insert new client
        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("insertclient")]
        public IHttpActionResult InsertClient(object param)
        {
            var request = JsonConvert.DeserializeObject<ClientRequest>(param.ToString());
            var result = _client.InsertClientDetails(request);
            return Json(result);
        }


        //modify client
        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("modifyclient")]
        public IHttpActionResult ModifyClient(object param)
        {
            var request = JsonConvert.DeserializeObject<ClientRequest>(param.ToString());
            var result = _client.ModifyClientDetails(request);
            return Json(result);
        }


        //////insert new brand
        ////[System.Web.Http.HttpPost]
        ////[System.Web.Http.Route("insertbrand")]
        ////public bool InsertBrand(Bra param)
        ////{
        ////    bool _clients = _brand.InsertBrand(param.BrandName, param.BrandDesc, param.IsBrandActive, param.CreatedBy, param.SystemIp, param.Computername);
        ////    if (_clients)
        ////    {
        ////        Log.InfoFormat(param.Computername, param.SystemIp, param.UserName, Constants.ActionType.CreateBrand.ToString());
        ////        _audit.InsertAudit(param.UserName, Constants.ActionType.CreateBrand.ToString(), "New Brand ", DateTime.Now, param.Computername, param.SystemIp);
        ////        return true;
        ////    }
        ////    else
        ////    {
        ////        Log.InfoFormat(param.Computername, param.SystemIp, param.UserName, Constants.ActionType.CreateBrand.ToString());
        ////        return false;
        ////    }

        ////}


        ////modify brand


        ////[System.Web.Http.HttpPost]
        ////[System.Web.Http.Route("modifybrand")]
        ////public bool ModifyBrand(BrandObj param)
        ////{
        ////    bool success = _brand.UpdateBrandDetails(param.BrandName, param.BrandDesc, param.IsBrandActive, param.CreatedBy, param.BrandId);
        ////    if (success)
        ////    {
        ////        Log.InfoFormat(param.Computername, param.SystemIp, param.UserName, Constants.ActionType.ModifyBrandDetails.ToString());
        ////        _audit.InsertAudit(param.UserName, Constants.ActionType.ModifyBrandDetails.ToString(), "Brand details changed", DateTime.Now, param.Computername, param.SystemIp);
        ////        return true;
        ////    }
        ////    else
        ////    {
        ////        Log.InfoFormat(param.Computername, param.SystemIp, param.UserName, Constants.ActionType.ModifyBrandDetails.ToString());
        ////        return false;
        ////    }
        ////}


        //insert new role menu


        //[System.Web.Http.HttpPost]
        //[System.Web.Http.Route("assignmenu")]
        //public bool InsertRoleMenu(RoleMenuObj param)
        //{
        //    var clients = new RoleMenu();
        //    clients.Itemid = param.ItemId;
        //    clients.Roleid = param.RoleId;
        //    clients.Menudesc = param.MenuDesc;
        //    bool success = _role.InsertRoleMenu(clients);
        //    if (success)
        //    {
        //        Log.InfoFormat(param.Computername, param.SystemIp, param.UserName, Constants.ActionType.AssignRoleMenu.ToString());
        //        _audit.InsertAudit(param.UserName, Constants.ActionType.AssignRoleMenu.ToString(), "Assign MenuRole", DateTime.Now, param.Computername, param.SystemIp);
        //        return true;
        //    }
        //    else
        //    {
        //        Log.InfoFormat(param.Computername, param.SystemIp, param.UserName, Constants.ActionType.AssignRoleMenu.ToString());
        //        return false;
        //    }

        //}


        ////modify role menu
        //[System.Web.Http.HttpPost]
        //[System.Web.Http.Route("modifyrolemenu")]
        //public bool ModifyRoleMenu(RoleMenuObj param)
        //{
        //    bool _clients = _role.UpdateRoleMenu(param);
        //    if (_clients)
        //    {
        //        Log.InfoFormat(param.Computername, param.SystemIp, param.UserName, Constants.ActionType.ModifyRoleMenu.ToString());
        //        _audit.InsertAudit(param.UserName, Constants.ActionType.ModifyRoleMenu.ToString(), "Assign Role Menu", DateTime.Now, param.Computername, param.SystemIp);
        //        return true;
        //    }
        //    else
        //    {
        //        Log.InfoFormat(param.Computername, param.SystemIp, param.UserName, Constants.ActionType.ModifyRoleMenu.ToString());
        //        return false;
        //    }

        //}


        //modify rolemenu
        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("allclient")]
        public IHttpActionResult ListClient()
        {
            var result = _client.ListAllClient();
            return Json(result);
        }


        //modify rolemenu
        //[System.Web.Http.HttpPost]
        //[System.Web.Http.Route("listrolemenu")]
        //public List<RoleMenuObj> ListRoleMenu(RoleMenuObj rolemenu)
        //{
        //    var role = _role.GetAllRoleMenu();
        //    return role;
        //}


        //modify rolemenu
        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("allusers")]
        public IHttpActionResult ListUser()
        {
            var result = _user.ListAvailableUser();
            return Json(result);
        }

        //insert new user
        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("newuser")]
        public IHttpActionResult InsertUser(object param)
        {
            var request = JsonConvert.DeserializeObject<UserDetailsRequest>(param.ToString());
            var result = _user.SetUpUserDetails(request);
            return Json(result);
        }


        //Return all Client details
        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("userdetails")]
        public IHttpActionResult UserDetails(object param)
        {
            //var request = JsonConvert.DeserializeObject<UserDetailsRequest>(param.ToString());
            var result = _user.RetrieveUserDetails(Convert.ToInt32(param));
            return Json(result); 
        }


        //insert new user
        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("modifyuser")]
        public IHttpActionResult ModifyUser(object param)
        {
            var request = JsonConvert.DeserializeObject<UserDetailsRequest>(param.ToString());
            var result = _user.ModifyUserInfo(request);
            return Json(result);
        }



        //modify rolemenu
        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("allterminals")]
        public IHttpActionResult ListTerminal()
        {
            var result = _terminal.GetAllTerminals();
            return Json(result);
        }


        //insert new user
        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("newterminal")]
        public IHttpActionResult InsertTerminal(object param)
        {
            var request = JsonConvert.DeserializeObject<TerminalRequest>(param.ToString());
            var result = _terminal.InsertTerminalDetails(request);
            return Json(result);
        }


        //Return all Client details
        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("terminaldetails")]
        public IHttpActionResult TerminalDetails(object param)
        {
            var request = JsonConvert.DeserializeObject<TerminalRequest>(param.ToString());
            var result = _terminal.TerminalDetails(request);
            return Json(result);
        }



        //insert new user
        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("bulkrecord")]
        public IHttpActionResult BulkRecord(object param)
        {
            var request = JsonConvert.DeserializeObject<List<UserDetailsBulkRequest>>(param.ToString());
            var result = _user.SaveBulkRecord(request);
            return Json(result);
        }



        //insert new user
        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("uploadterminal")]
        public IHttpActionResult UploadTerminal(object param)
        {
            var request = JsonConvert.DeserializeObject<List<TerminalBulkRequest>>(param.ToString());
            var result = _terminal.UploadBulkTerminalRecords(request);
            return Json(result);
        }


        //Modify terminal record
        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("modifyterminal")]
        public IHttpActionResult ModifyTerminal(object param)
        {
            var request = JsonConvert.DeserializeObject<TerminalRequest>(param.ToString());
            var result = _terminal.ModifyTerminalDetails(request);
            return Json(result);
        }


        //modify rolemenu
        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("onlyclients")]
        public IHttpActionResult ListOnlyCustomer()
        {
            var request = _client.ListOnlyCustomerRole();
            return Json(request);
        }

        //modify rolemenu
        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("allstates")]
        public IHttpActionResult ListStates()
        {
            var request = _terminal.ListAvailableStates();
            return Json(request);
        }

        //modify rolemenu
        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("allengineers")]
        public IHttpActionResult ListEngineers()
        {
            var request = _user.ListAvailableEngineers();
            return Json(request);
        }

        ////modify rolemenu
        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("allbrands")]
        public IHttpActionResult ListBrands()
        {
            var request = _brand.ListAllAvailableBrands();
            return Json(request);
        }


        ////Lists pending calls
        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("newrequest")]
        public IHttpActionResult NewRequest(object param)
        {
            var request = JsonConvert.DeserializeObject<IncidentLogRequest>(param.ToString());
            var result = _incident.LogNewRequest(request);
            return Json(result);
        }


        ////Lists pending calls
        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("listrequest")]
        public IHttpActionResult AllPendingRequest()
        {
            var result = _incident.AllPendingRequest();
            return Json(result);
        }

    }
}