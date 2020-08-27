using PremiumCalculatorUI.Filters;
using System.Web.Mvc;

namespace PremiumCalculatorUI
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
            filters.Add(new ExceptionHandlingFilter());
        }
    }
}
