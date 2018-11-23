using System.Web.Mvc;
using System.Web.Routing;

namespace AsArch.NET
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            //routes.MapMvcAttributeRoutes();
            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}/{order}",
                defaults: new { controller = "Nodes"/*"Home"*/, action = "Index", id = UrlParameter.Optional, order = UrlParameter.Optional }
            );
        }
    }
}
