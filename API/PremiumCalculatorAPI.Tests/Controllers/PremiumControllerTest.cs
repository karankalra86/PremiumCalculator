using System;
using System.Net.Http;
using System.Web.Http;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using PremiumCalculatorAPI.Controllers;
using PremiumCalculatorAPI.Models;
using PremiumCalculatorData.Repository;
using Rhino.Mocks;

namespace PremiumCalculatorAPI.Tests.Controllers
{
    [TestClass]
    public class PremiumControllerTest
    {
        [TestMethod]
        public void CalculatePremiumTest()
        {
            MemberModel objMemberModel = new MemberModel
            {
                Name = "TestName",
                Age = 20,
                DateOfBirth = DateTime.Now,
                Occupation = "Doctor",
                SumInsured = 200000
            };

            decimal expectedOccupationratingfactor = 1.5m;
            decimal expectedPremium = (objMemberModel.SumInsured * expectedOccupationratingfactor * objMemberModel.Age) / 1000 * 12;


            // Arrange
            var mockManager = MockRepository.GenerateMock<IOccupationRepository>();
            mockManager.Expect(x => x.GetOccupationRatingFactor(null)).IgnoreArguments().Return(expectedOccupationratingfactor).Repeat.Any();

            // Act
            var objPremiumController = new PremiumController(mockManager)
            {
                Request = new HttpRequestMessage(),
                Configuration = new HttpConfiguration()
            };
            objPremiumController.Request = new HttpRequestMessage();
            var result = objPremiumController.CalculatePremium(objMemberModel);

            // Assert
            Assert.AreEqual("OK", result.StatusCode.ToString());
            var actualPremium = JsonConvert.DeserializeObject<decimal>(result.Content.ReadAsStringAsync().Result);
            Assert.AreEqual(expectedPremium, actualPremium);
            mockManager.VerifyAllExpectations();
        }

    }
}
