using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MvcMusicStore.Controllers;
using NLog;
using Autofac;
using Autofac.Integration.Mvc;
using System.Web.Mvc;
using PerformanceCounterHelper;
using MvcMusicStore.Infrastructure;

namespace MvcMusicStore.DI
{
    public class AutofacConfig
    {
        public static void ContainerConfig()
            {
            var builder = new ContainerBuilder();
            builder.RegisterControllers(typeof(MvcApplication).Assembly);
            builder.Register(r => LogManager.GetLogger("ControllerLoggers")).As<ILogger>();
            var container = builder.Build();
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
        }
    }
}