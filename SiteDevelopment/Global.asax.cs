using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using SiteDevelopment.Models;
using SiteDevelopment.Repository;

namespace SiteDevelopment
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            ModelBinders.Binders.Add(typeof(Match), new EnumPropertyBinder());
            ModelBinders.Binders.Add(typeof(InputData), new EnumPropertyBinder());
            ModelBinders.Binders.Add(typeof (News), new TagsPropertyBinder());
        }
    }
}
