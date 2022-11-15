using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using WebTimeSheetManagement.Filters;
using Azure.Security.KeyVault.Secrets;
using WebTimeSheetManagement.Models;

namespace WebTimeSheetManagement
{
    public class MvcApplication : System.Web.HttpApplication
    {
        SecretClient secretclient = new SecretClient(new Uri("https://DevEnv19DataKeyVault.vault.azure.net"),new Azure.Identity.DefaultAzureCredential());
        public string constrstring;
        protected void Application_BeginRequest()
        {
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.Cache.SetExpires(DateTime.UtcNow.AddHours(-1));
            Response.Cache.SetNoStore();
        }

        protected void Application_Start()        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            GlobalFilters.Filters.Add(new UserAuditFilter());
            
            GlobalConnectionString.connectionString = secretclient.GetSecret("TimesheetDBEntities").Value.Value;
            SqlDependency.Start(secretclient.GetSecret("TimesheetDBEntities").Value.Value);

           
        }


        void Application_Error(object sender, EventArgs e)
        {

            Exception ex = Server.GetLastError();
            if (ex == null || ex.Message.StartsWith("File"))
            {
                return;
            }
            try
            {
                Server.ClearError();
                Response.Redirect("~/Errorview/Error");
            }
            finally
            {
                ex = null;
            }

        }
    }
}
