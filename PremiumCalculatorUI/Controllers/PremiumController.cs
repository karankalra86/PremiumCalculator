using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Mvc;
using PremiumCalculatorWrapper;
using PremiumCalcModels;
using PremiumCalculatorUI.ViewModels;
using System.Configuration;

namespace PremiumCalculatorUI.Controllers
{
    public class PremiumController : Controller
    {
        private IPremiumCalcWrapper _iPremiumCalcWrapper;

        public string ApiURL { get; set; }

        public PremiumController()
        {
            ApiURL = ConfigurationManager.AppSettings["PremiumCalcApiBaseURL"] + ConfigurationManager.AppSettings["OccupationURL"];
            _iPremiumCalcWrapper = new PremiumCalcWrapper(ApiURL);
        }

        public PremiumController(IPremiumCalcWrapper iPremiumCalcWrapper)
        {
            _iPremiumCalcWrapper = iPremiumCalcWrapper;
        }


        [HttpGet]
        public ActionResult Premium(MemberModel objMemberModel)
        {
            var memberViewModel = GetOccupations();
            return View(memberViewModel);
        }
        
        private MemberViewModel GetOccupations()
        {
            Task<List<string>> occupationList = _iPremiumCalcWrapper.GetOccupations();
            MemberViewModel objMemberModel = new MemberViewModel
            {
                Occupation = occupationList.Result
            };
            return objMemberModel;
        }

     
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Premium(MemberViewModel objMemberViewModel)
        {
            if (ModelState.IsValid)
            {
                var memberModel = MapViewModelToModel(objMemberViewModel);
                Task<decimal> premiumValue = _iPremiumCalcWrapper.CalculatePremium(memberModel);
                premiumValue.Wait();
                objMemberViewModel.MonthlyPremium = premiumValue.Result;
            }
            return View(objMemberViewModel);
        }

        private MemberModel MapViewModelToModel(MemberViewModel objMemberViewModel)
        {
            MemberModel objMemberModel = new MemberModel();
            return AutoMapper.Mapper.Map(objMemberViewModel, objMemberModel);
        }
    }
}