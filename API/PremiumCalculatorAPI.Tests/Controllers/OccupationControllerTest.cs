using System.Collections.Generic;
using System.Net.Http;
using System.Web.Http;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using PremiumCalculatorAPI.Controllers;
using PremiumCalculatorData.Repository;
using Rhino.Mocks;

namespace PremiumCalculatorAPI.Tests.Controllers
{
    [TestClass]
    public class OccupationControllerTest
    {
        [TestMethod]
        public void GetOccupationsTest()
        {
            List<string> expectedOccupations = new List<string>() { "Florist", "Mechanic", "Farmer" };

            // Arrange
            var mockManager = MockRepository.GenerateMock<IOccupationRepository>();
            mockManager.Expect(x => x.GetOccupations()).IgnoreArguments().Return(expectedOccupations).Repeat.Any();

            // Act
            var objOccupationController = new OccupationController(mockManager)
            {
                Request = new HttpRequestMessage(),
                Configuration = new HttpConfiguration()
            };
            objOccupationController.Request = new HttpRequestMessage();
            var result = objOccupationController.GetOccupations();

            // Assert
            Assert.AreEqual("OK", result.StatusCode.ToString());
            var actualResult = JsonConvert.DeserializeObject<List<string>>(result.Content.ReadAsStringAsync().Result);
            Assert.AreEqual(expectedOccupations.Count, actualResult.Count);
            mockManager.VerifyAllExpectations();
        }
    }
}
