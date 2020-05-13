using UITestNetCore.Helpers;
using NUnit.Framework;
using Castle.DynamicProxy;
using System.Collections.Generic;

namespace UITestNetCore.Bases
{
    public class TestBase
    {
        [SetUp]
        public void Setup()
        {
            ExtentReportHelpers.AddTest();
            DriverFactory.CreateInstance();
            DriverFactory.INSTANCE.Navigate().GoToUrl(BuilderJson.ReturnParameterAppSettings("DEFAULT_APPLICATION_URL"));
        }

        [TearDown]
        public void TearDown()
        {
            ExtentReportHelpers.AddTestResult();
            DriverFactory.QuitInstace();
        }

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            ExtentReportHelpers.CreateReport();
        }

        [OneTimeTearDown]
        public void OneTimeTearDown()
        {
             ExtentReportHelpers.GenerateReport();
        }
    }
}
