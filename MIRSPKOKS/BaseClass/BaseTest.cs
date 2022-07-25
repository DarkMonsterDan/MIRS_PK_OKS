using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestFramework.Core;
using log4net;
using log4net.Config;


namespace MIRSPKOKS.BaseClass
{
   public class BaseTest 
    {
       
        public IWebDriver driver;
        public ILog Log;
        [SetUp]
       

        public void Open()
        {
            Log = LogManager.GetLogger(GetType());
            ChromeOptions options = new ChromeOptions();
            options.AddExtension(@"..\..\..\Resources\1.2.8_0.crx");
            driver = new ChromeDriver(options);        
            driver.Manage().Window.Maximize();
            driver.Url = "http://stk-lk.permkrai.ru/loginback/";
        }

        [TearDown]
        public void Close()
        {
            driver.Quit();

        }
    }
}
