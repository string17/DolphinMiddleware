using log4net;
using Microsoft.Owin;
using Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;


[assembly: OwinStartup(typeof(DolphinMiddleware.Startup))]

namespace DolphinMiddleware
{
    public class Startup
    {
        private static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        public void Configuration(IAppBuilder app)
        {
            Log.InfoFormat("Middleware started successfully!");
        }
    }
}