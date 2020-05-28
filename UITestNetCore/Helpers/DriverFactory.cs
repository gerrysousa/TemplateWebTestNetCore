using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Text;

namespace UITestNetCore.Helpers
{
    public class DriverFactory
    {
        public static IWebDriver INSTANCE { get; set; } = null;

        public static void CreateInstance()
        {
            string browser = BuilderJson.ReturnParameterAppSettings("BROWSER");
            string execution = BuilderJson.ReturnParameterAppSettings("EXECUTION");
            bool headless = bool.Parse(BuilderJson.ReturnParameterAppSettings("HEADLESS"));

            if (INSTANCE == null)
            {
                switch (browser)
                {
                    case "chrome":
                        if (execution.Equals("local"))
                        {
                            INSTANCE = headless ? Browsers.GetLocalChromeHeadless() : Browsers.GetLocalChrome();
                        }

                        if (execution.Equals("remota"))
                        {
                            INSTANCE = headless ? Browsers.GetRemoteChromeHeadless() : Browsers.GetRemoteChrome();
                        }

                        if (execution.Equals("azure"))
                        {
                            INSTANCE = headless ? Browsers.GetChromeAzure() : Browsers.GetChromeAzure();
                        }

                        break;

                    case "ie":
                        if (execution.Equals("local"))
                        {
                            INSTANCE = Browsers.GetLocalInternetExplorer();
                        }

                        if (execution.Equals("remota"))
                        {
                            INSTANCE = Browsers.GetRemoteInternetExplorer();
                        }

                        break;

                    case "firefox":
                        if (execution.Equals("local"))
                        {
                            INSTANCE = Browsers.GetLocalFirefox();
                        }

                        if (execution.Equals("remota"))
                        {
                            INSTANCE = Browsers.GetRemoteFirefox();
                        }

                        break;

                    case "edge":
                        if (execution.Equals("local"))
                        {
                            INSTANCE = Browsers.GetLocalEdge();
                        }

                        if (execution.Equals("remota"))
                        {
                            INSTANCE = Browsers.GetRemoteEdge();
                        }

                        break;

                    default:
                        throw new Exception("O browser informado não existe ou não é suportado pela automação");
                }
            }
        }

        public static void QuitInstace()
        {
            INSTANCE.Quit();
            INSTANCE.Dispose();
            INSTANCE = null;
        }
    }
}
