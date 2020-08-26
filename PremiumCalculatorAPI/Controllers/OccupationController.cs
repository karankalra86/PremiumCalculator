using PremiumCalculatorData.Repository;
using System;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace PremiumCalculatorAPI.Controllers
{
    /// <summary>
    /// OccupationController class
    /// </summary>
    public class OccupationController : ApiController
    {
        IOccupationRepository _repository;

        /// <summary>
        /// 
        /// </summary>
        public OccupationController()
        {
            _repository = new OccupationRepository();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="objOccupationRepo"></param>
        public OccupationController(IOccupationRepository objOccupationRepo)
        {
            _repository = objOccupationRepo;
        }

        /// <summary>
        /// This API gets the list of all available Occupations
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public HttpResponseMessage GetOccupations()
        {
            try
            {
                var message = Request.CreateResponse(HttpStatusCode.OK, _repository.GetOccupations());
                return message;
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }
    }
}