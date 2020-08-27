using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Mvc;
using PremiumCalculatorWrapper;
using PremiumCalcModels;
using PremiumCalculatorUI.ViewModels;
using System.Configuration;
using System;

namespace PremiumCalculatorUI.Controllers
{
    public class PremiumController : Controller
    {
        private IPremiumCalcWrapper _iPremiumCalcWrapper;

        public string BaseURL { get; set; }
        public string OperationURL { get; set; }

        public PremiumController()
        {
            BaseURL = ConfigurationManager.AppSettings["PremiumCalcApiBaseURL"];
            _iPremiumCalcWrapper = new PremiumCalcWrapper(BaseURL);
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