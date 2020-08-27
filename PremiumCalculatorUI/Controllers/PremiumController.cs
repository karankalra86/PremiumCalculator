using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Mvc;
using PremiumCalculatorWrapper;
using PremiumCalcModels;
using PremiumCalculatorUI.ViewModels;
using System.Configuration;
using PremiumCalculatorLogger;

namespace PremiumCalculatorUI.Controllers
{
    public class PremiumController : Controller
    {
        private IPremiumCalcWrapper _iPremiumCalcWrapper;
        private ILogger _iLogger;

        public string OperationURL { get; set; }

        public PremiumController(IPremiumCalcWrapper iPremiumCalcWrapper, ILogger logger)
        {
            _iPremiumCalcWrapper = iPremiumCalcWrapper;
            _iLogger = logger;
        }

        [HttpGet]
        public ActionResult Premium(MemberModel objMemberModel)
        {
            _iLogger.WriteLog("Premium Calculator : Starting fetching occupations", false);
            var memberViewModel = GetOccupations();
            _iLogger.WriteLog("Premium Calculator : Returning occupations", false);
            return View(memberViewModel);
        }
        
        private MemberViewModel GetOccupations()
        {
            _iPremiumCalcWrapper.OperationURL = ConfigurationManager.AppSettings["OccupationURL"];
            Task<List<string>> occupationList = _iPremiumCalcWrapper.GetOccupations();
            MemberViewModel objMemberModel = new MemberViewModel
            {
                OccupationList = occupationList.Result
            };
            TempData["occupationList"] = occupationList.Result;
            return objMemberModel;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Premium(MemberViewModel objMemberViewModel)
        {
            _iPremiumCalcWrapper.OperationURL = ConfigurationManager.AppSettings["CalculatePremiumURL"];
            objMemberViewModel.OccupationList = (List<string>)TempData["occupationList"];   
            if (ModelState.IsValid)
            {
                var memberModel = MapViewModelToModel(objMemberViewModel);
                Task<decimal> premiumValue = _iPremiumCalcWrapper.CalculatePremium(memberModel);
                premiumValue.Wait();
                objMemberViewModel.Premium = premiumValue.Result;
            }
            TempData["occupationList"] = objMemberViewModel.OccupationList;
            return View(objMemberViewModel);
        }

        private MemberModel MapViewModelToModel(MemberViewModel objMemberViewModel)
        {
            MemberModel objMemberModel = new MemberModel();
            return AutoMapper.Mapper.Map(objMemberViewModel, objMemberModel);
        }
    }
}