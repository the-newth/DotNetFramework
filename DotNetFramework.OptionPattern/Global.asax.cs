using DotNetFramework.OptionPattern.Models;
using DotNetFramework.OptionPattern.Utils;
using SimpleInjector;
using SimpleInjector.Integration.Web.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace DotNetFramework.OptionPattern
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            var container = new Container();

            container.RegisterMvcControllers(Assembly.GetExecutingAssembly());

            //Injection form Web.config settings to Option Model  
            container.Register<IOptions<SecretOptions>, Options<SecretOptions>>(SimpleInjector.Lifestyle.Singleton);

            DependencyResolver.SetResolver(new SimpleInjectorDependencyResolver(container));

        }
    }
}
