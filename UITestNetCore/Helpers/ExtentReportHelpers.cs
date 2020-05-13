using AventStack.ExtentReports;
using AventStack.ExtentReports.Reporter;
using NUnit.Framework;
using NUnit.Framework.Interfaces;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace UITestNetCore.Helpers
{
    public class ExtentReportHelpers
    {
        #region parameters
        public static AventStack.ExtentReports.ExtentReports EXTENT_REPORT = null;
        public static ExtentTest TEST;
        static string reportName = BuilderJson.ReturnParameterAppSettings("REPORT_NAME") + "_" + DateTime.Now.ToString("dd-MM-yyyy_HH-mm");

        static string projectBinDebugPath = AppDomain.CurrentDomain.BaseDirectory;
        static FileInfo fileInfo = new FileInfo(projectBinDebugPath);
        static DirectoryInfo projectFolder = fileInfo.Directory;
        static string projectFolderPath = projectFolder.FullName;
        static string reportRootPath = projectFolderPath + "/Reports/";
        static string reportPath = projectFolderPath + "/Reports/" + reportName + "/";
        static string fileName = reportName + ".html";
        static string fullReportFilePath = reportPath + "_" + fileName;
        #endregion parameters

        public static void CreateReport()
        {
            if (EXTENT_REPORT == null)
            {
                var htmlReporter = new ExtentHtmlReporter(reportPath);
                EXTENT_REPORT = new AventStack.ExtentReports.ExtentReports();
                EXTENT_REPORT.AttachReporter(htmlReporter);
            }
        }

        public static void AddTest()
        {
            string testName = TestContext.CurrentContext.Test.MethodName;


            TEST = EXTENT_REPORT.CreateTest(testName);
        }

        public static void AddTestInfo(int methodLevel, string text)
        {
            if (BuilderJson.ReturnParameterAppSettings("GET_SCREENSHOT_FOR_EACH_STEP").ToString() == "true")
            {
                TEST.Log(Status.Pass, GeneralHelpers.GetMethodNameByLevel(methodLevel) + " || " + text, GetScreenShotMedia());
            }
            else
            {
                TEST.Log(Status.Pass, GeneralHelpers.GetMethodNameByLevel(methodLevel) + " || " + text);
            }
        }

        public static MediaEntityModelProvider GetScreenShotMedia()
        {
            string screenshotPath = GetScreenshot(reportPath);
            TestContext.AddTestAttachment(screenshotPath);
            return MediaEntityBuilder.CreateScreenCaptureFromPath(screenshotPath.Replace(reportPath, ".")).Build();
        }

        public static void AddTestResult()
        {
            var status = TestContext.CurrentContext.Result.Outcome.Status;
            var message = string.IsNullOrEmpty(TestContext.CurrentContext.Result.Message) ? "" : string.Format("{0}", TestContext.CurrentContext.Result.Message);
            var stacktrace = string.IsNullOrEmpty(TestContext.CurrentContext.Result.StackTrace) ? "" : string.Format("{0}", TestContext.CurrentContext.Result.StackTrace);

            switch (status)
            {
                case TestStatus.Failed:
                    TEST.Log(Status.Fail, "Test Result: " + Status.Fail + "<pre>" + "Message: " + message + "</pre>" + "<pre>" + "Stack Trace: " + stacktrace + "</pre>", GetScreenShotMedia());
                    break;
                case TestStatus.Inconclusive:
                    TEST.Log(Status.Warning, "Test Result: " + Status.Warning + "<pre>" + "Message: " + message + "</pre>" + "<pre>" + "Stack Trace: " + stacktrace + "</pre>", GetScreenShotMedia());
                    break;
                case TestStatus.Skipped:
                    TEST.Log(Status.Skip, "Test Result: " + Status.Skip + "<pre>" + "Message: " + message + "</pre>" + "<pre>" + "Stack Trace: " + stacktrace + "</pre>", GetScreenShotMedia());
                    break;
                default:
                    TEST.Log(Status.Pass, "Test Result: " + Status.Pass);
                    break;
            }
        }

        public static void GenerateReport()
        {
            EXTENT_REPORT.Flush();
        }

        public static string GetScreenshot(string path)
        {
            string testName = TestContext.CurrentContext.Test.MethodName;
            string date = DateTime.Now.ToString().Replace("/", "_").Replace(":", "_").Replace(" ", "-");

            Screenshot screenShot = ((ITakesScreenshot)DriverFactory.INSTANCE).GetScreenshot();
            string filePathAndName = path + "/" + testName + "_" + date + ".png";
            screenShot.SaveAsFile(filePathAndName, ScreenshotImageFormat.Png);

            return filePathAndName;
        }

        public static void AddTestScreenshot(int methodLevel, string text)
        {
            TEST.Log(Status.Pass, GeneralHelpers.GetMethodNameByLevel(methodLevel) + " || " + text, GetScreenShotMedia());
        }
    }
}
