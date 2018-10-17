using System.Web.Mvc;
using System.Web.Routing;

namespace AsArch.NET
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            #region удалить
            //routes.MapRoute(
            //    name: "Comments",
            //    url: "comments",
            //    defaults: new { controller = "Home", action = "Comments" }
            //);

            //routes.MapRoute(
            //    name: "NewComment",
            //    url: "comments/new",
            //    defaults: new { controller = "Home", action = "AddComment" }
            //);
            #endregion

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
