using System.Web.Mvc;
using System.Web.Routing;

namespace ConnectionChecker
{
    /// <summary>
    /// The route config.
    /// </summary>
    public class RouteConfig
    {
        /// <summary>
        /// The register routes.
        /// </summary>
        /// <param name="routes">
        /// The routes.
        /// </param>
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Connection", action = "Index", id = UrlParameter.Optional });

            routes.MapRoute(
                "404-PageNotFound",
                "{*url}",
                new { controller = "StaticContent", action = "PageNotFound" });
        }
    }
}
