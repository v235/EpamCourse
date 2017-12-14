using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using PerformanceCounterHelper;
using MvcMusicStore.Infrastructure;
using MvcMusicStore.DI;
using NLog;

namespace MvcMusicStore
{
    public class MvcApplication : System.Web.HttpApplication
    {
        private readonly ILogger logger;

        public MvcApplication()
        {
            this.logger = LogManager.GetCurrentClassLogger();
        }
        protected void Application_Start()
        {
            //DI
            AutofacConfig.ContainerConfig();

            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            logger.Info("Application started");


            //Counters set to zero
            using (var counterHelper =
                PerformanceHelper.CreateCounterHelper<Counters>("Monitoring project"))
            {
                counterHelper.RawValue(Counters.SuccessLogin, 0);
                counterHelper.RawValue(Counters.SuccessLogoff, 0);
                counterHelper.RawValue(Counters.AddsToCart, 0);
                logger.Info("All counters set to zero");
            }
        }
        protected void Application_Error()
        {
            //logger.Log(logEvent(Error), "");
            logger.Error(Server.GetLastError().ToString());
        }
    }
}
