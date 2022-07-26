using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Support;
using OpenQA.Selenium.Interactions;
using SeleniumExtras.PageObjects;
using OpenQA.Selenium.Chrome;
using System.Threading;
using MIRSPKOKS.PagesQA;
using TestFramework.Core;
using MIRSPKOKS.BaseClass;
using System;
using NUnit.Allure.Attributes;
using NUnit.Allure.Core;
using Allure.Commons;

namespace MIRSPKOKS 
{

   
    [TestFixture]
    [AllureNUnit]
    public class Tests : BaseTest
    {
        [OneTimeSetUp]
        public void ClearResultsDir()
        {
            AllureLifecycle.Instance.CleanupResultDirectory();
        }

    
        [Test]
        [AllureTag("NUnit", "Debug")]
        [AllureSeverity(SeverityLevel.critical)]
        [AllureFeature("Core")]
        
        public void SignatureAktViezdnogo()
        {
            
            Authorization Authorization = new Authorization(); // чтобы могли обращаться к объектам из PageHome.cs                                                  
            PageFactory.InitElements(driver, Authorization); // инициализация элементов Page Object из PageHome.cs
            MainPage MainPage = new MainPage();            
            PageFactory.InitElements(driver, MainPage);    
            Authorization.Login.SendKeys("IGSN4");
            Authorization.Password.SendKeys("IGSN4"); 
            Authorization.Input.Click();                
            driver.SwitchTo().Frame(MainPage.Iframe_test);
            MainPage.Nadzor.Click();   
            driver.SwitchTo().Frame(MainPage.Iframe_test2);
            driver.SwitchTo().Frame(MainPage.Iframe_test3);
            MainPage.Test_line_object.Click();
            MainPage.Edit_button_object.Click();           
            Thread.Sleep(6000);
            MainPage.Akt_Proverki.Click();        
            driver.SwitchTo().Frame(MainPage.Iframe_test4);
            MainPage.Akt_Viezdnogo.Click();
            MainPage.Edit_button_akt.Click();        
             Thread.Sleep(20000);
            MainPage.Zhurnal.Click();        
            MainPage.Podpis.Click();        
            MainPage.Radio_button_Avtomat.Click();      
            MainPage.Ok_button.Click();     
            MainPage.Sertif_petrov.Click();
            MainPage.Vibrat_button.Click();
            
            var check = MainPage.Text_podpis.Text;
            if (check.Contains("Подписано"))// проверяем успешно ли
            {
                Assert.Fail("Возникла ошибка при подписании");
            }
            else
            {
                Assert.Fail("Возникла ошибка при подписании");
                driver.Quit();
            }

        }

        [Test]
        [AllureTag("NUnit", "Debug")]
        [AllureSeverity(SeverityLevel.critical)]
        [AllureFeature("Core")]
        public void test2()
        {

            Authorization Authorization = new Authorization(); // чтобы могли обращаться к объектам из PageHome.cs                                                  
            PageFactory.InitElements(driver, Authorization); // инициализация элементов Page Object из PageHome.cs
            MainPage MainPage = new MainPage();
            PageFactory.InitElements(driver, MainPage);

            Authorization.Login.SendKeys("IGSN4");
           // if (!Authorization.Login.Displayed)
            //    throw new Exception("Поле Логин отсутствует на форме");
          //  Log.Info("Поле Логин заполнилось");
            Authorization.Password.SendKeys("IGSN4");
            // AllureLifecycle.Instance.WrapInStep(() =>

            Assert.IsTrue(Authorization.Input.Displayed, "успешно");
           // Assert.IsTrue(false, "Сломал намеренно");



        }
    }
}