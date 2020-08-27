using PremiumCalculatorAPI.Models;
using PremiumCalculatorData.Repository;
using System;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace PremiumCalculatorAPI.Controllers
{
    /// <summary>
    /// PremiumController Class
    /// </summary>
    public class PremiumController : ApiController
    {
        IOccupationRepository _repository;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="objOccupationRepo"></param>
        public PremiumController(IOccupationRepository objOccupationRepo)
        {
            _repository = objOccupationRepo;
        }

        /// <summary>
        /// This API calculates the premium for a member based on entioned formula
        /// Death Premium = (Death cover amont * Occupation Rating Factor * Age) / 1000 * 12
        /// </summary>
        /// <param name="objMemberModel"></param>
        /// <returns></returns>
        [HttpPost]
        public HttpResponseMessage CalculatePremium([FromBody]MemberModel objMemberModel)
        {
            try
            {
                var message = Request.CreateResponse(HttpStatusCode.OK, (objMemberModel.SumInsured * GetOccupationRatingFactor(objMemberModel.Occupation) * objMemberModel.Age) / 1000 * 12);
                return message;
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, ex);
            }
        }

        private decimal GetOccupationRatingFactor(string strOccupation)
        {
            return _repository.GetOccupationRatingFactor(strOccupation);
        }
    }
}