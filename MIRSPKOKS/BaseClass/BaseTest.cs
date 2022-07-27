using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestFramework.Core;


namespace MIRSPKOKS.BaseClass
{
   public class BaseTest 
    {
       
        public IWebDriver driver;
      

        [SetUp]
       

        public void Open()
        {
   
            ChromeOptions options = new ChromeOptions();
            options.AddExtension(@"..\..\..\Resources\1.2.8_0.crx");
            driver = new ChromeDriver(options);        
            driver.Manage().Window.Maximize();
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(20);
          
            driver.Url = "http://stk-lk.permkrai.ru/loginback/";

        }

        [TearDown]
        public void Close()
        {
            driver.Quit();

        }
    }
}
