using System.Web.Mvc;
using System.Web.Routing;

namespace AsArch.NET
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

           // routes.MapRoute(
           //    name: "GetDopPredIskJson",
           //    url: "getdopprediskjson",
           //    defaults: new { controller = "Nodes", action = "GetDopPredIskJson" }
           //);

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Nodes"/*"Home"*/, action = "Index", id = UrlParameter.Optional }
            );
            //routes.MapRoute(
            //  name: "FormRoute",
            //  url: "app/forms/{controller}/{action}"
            //);
        }
    }
}
