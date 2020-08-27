using System.Web.Mvc;
using System.Web.Routing;

namespace PremiumCalculatorAPI
{
    /// <summary>
    /// 
    /// </summary>
    public class RouteConfig
    {
        /// <summary>
        /// 
        /// </summary>
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Occupation", action = "GetOccupations", id = UrlParameter.Optional }
            );
        }
    }
}
