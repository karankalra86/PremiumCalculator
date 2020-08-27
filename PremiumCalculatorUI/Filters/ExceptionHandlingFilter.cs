using System.Web.Mvc;
using PremiumCalculatorLogger;
using PremiumCalculatorUI.Helpers;

namespace PremiumCalculatorUI.Filters
{
    public class ExceptionHandlingFilter : ActionFilterAttribute, IExceptionFilter
    {
        private readonly ILogger _logger;

        public ExceptionHandlingFilter()
        {
            _logger = DependencyResolver.Current.GetService<ILogger>();
        }

        public void OnException(ExceptionContext filterContext)
        {
            if (!filterContext.ExceptionHandled)
            {
                try
                {
                    string errorMsg;
                    var url = filterContext.HttpContext.Request.UrlReferrer != null
                        ? filterContext.HttpContext.Request.UrlReferrer.PathAndQuery
                        : string.Empty;
                    errorMsg = filterContext.Exception.Message;

                    _logger.WriteLog(GlobalHelper.FormatError(filterContext.Exception), true);
                    if (filterContext.Exception is HttpAntiForgeryException)
                        _logger.WriteLog(string.Format("Anti forgery exeption {0} {1}", url, errorMsg), true);

                    filterContext.Result = new RedirectResult("CustomError.html");
                    filterContext.ExceptionHandled = true;
                }
                catch
                {
                    _logger.WriteLog("Generic error", true);
                }
            }
        }
    }
}