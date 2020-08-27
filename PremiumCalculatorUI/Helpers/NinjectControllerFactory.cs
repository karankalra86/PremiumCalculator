using Ninject;
using PremiumCalculatorLogger;
using PremiumCalculatorWrapper;
using System;
using System.Configuration;
using System.Web.Mvc;
using System.Web.Routing;

namespace PremiumCalculatorUI.Helpers
{
    public class NinjectControllerFactory : DefaultControllerFactory
    {
        private IKernel ninjectKernel;
        public NinjectControllerFactory()
        {
            ninjectKernel = new StandardKernel();
            AddBindings();
        }
        protected override IController GetControllerInstance(RequestContext requestContext, Type controllerType)
        {
            return controllerType == null ? null : (IController)ninjectKernel.Get(controllerType);
        }
        private void AddBindings()
        {
            // add your bindings here as required    
            ninjectKernel.Bind<ILogger>().To<EventLogger>();
            
            ninjectKernel.Bind<IPremiumCalcWrapper>().To<PremiumCalcWrapper>()
                .WithConstructorArgument("strBaseURL",ConfigurationManager.AppSettings["PremiumCalcApiBaseURL"].ToString());
        }
    }
}