using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Mvc;
using AutoMapper;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PremiumCalcModels;
using PremiumCalculatorLogger;
using PremiumCalculatorUI.Controllers;
using PremiumCalculatorUI.ViewModels;
using PremiumCalculatorWrapper;
using Rhino.Mocks;

namespace PremiumCalculatorUI.Tests.Controllers
{
    [TestClass]
    public class UIPremiumControllerTest
    {
        IPremiumCalcWrapper mocWrapper;
        ILogger mocLogger;

        [TestInitialize]
        public void MyTestInitialize()
        {
            mocWrapper = MockRepository.GenerateMock<IPremiumCalcWrapper>();
            mocLogger = MockRepository.GenerateMock<ILogger>();
            mocLogger.Stub(x => x.WriteLog(string.Empty, false)).IgnoreArguments();
        }

        [TestMethod]
        public void CalculatePremiumTest()
        {
            MemberViewModel objMemberViewModel = new MemberViewModel
            {
                Name = "TestName",
                Age = 20,
                DateOfBirth = DateTime.Now,
                Occupation = "Doctor",
                SumInsured = 200000
            };

            decimal expectedOccupationratingfactor = 1.5m;
            
            decimal expectedPremium = (objMemberViewModel.SumInsured * expectedOccupationratingfactor * objMemberViewModel.Age) / 1000 * 12;


            // Arrange
            mocWrapper.Expect(x => x.CalculatePremium(null)).IgnoreArguments().Return(Task.FromResult(expectedPremium)).Repeat.Any();
            

            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<MemberViewModel, MemberModel>();
            });

            // Act
            var objPremiumController = new PremiumController(mocWrapper, mocLogger);
            var result = objPremiumController.Premium(objMemberViewModel);

            // Assert
            var actualPremium = ((MemberViewModel)((ViewResultBase)result).Model).Premium;
            Assert.AreEqual(expectedPremium, actualPremium);
            mocWrapper.VerifyAllExpectations();
        }

        [TestMethod]
        public void GetOccupationsTest()
        {
            List<string> expectedOccupations = new List<string>() { "Florist", "Mechanic", "Farmer" };

            // Arrange
            mocWrapper.Expect(x => x.GetOccupations()).IgnoreArguments().Return(Task.FromResult(expectedOccupations)).Repeat.Any();

            // Act
            var objPremiumController = new PremiumController(mocWrapper, mocLogger);
            var result = objPremiumController.Premium(new MemberModel());

            // Assert
            Assert.AreEqual(expectedOccupations.Count, ((MemberViewModel)((ViewResultBase)result).Model).OccupationList.Count);
            mocWrapper.VerifyAllExpectations();
        }
    }
}
