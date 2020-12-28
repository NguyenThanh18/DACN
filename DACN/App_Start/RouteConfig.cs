using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace DACN
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "HomePage", id = UrlParameter.Optional },
                namespaces: new[] { "DACN.Controllers" }
            );
            routes.MapRoute(
                name: "Detail",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Posting", action = "Detail", id = UrlParameter.Optional },
                namespaces: new[] { "DACN.Controllers" }
            );
        }
    }
}
