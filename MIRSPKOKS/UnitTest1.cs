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

using OpenQA.Selenium.Support.UI;

namespace MIRSPKOKS 
{

   
    [TestFixture]
    [AllureNUnit]
    public class Tests : BaseTest
    {
        //public static void Wait(int miliseconds, int maxTimeOutSeconds = 60)
        //{
        //    var wait = new WebDriverWait(driver, timeout: new TimeSpan(0, 0, 1, maxTimeOutSeconds));
        //    var delay = new TimeSpan(0, 0, 0, 0, miliseconds);
        //    var timestamp = DateTime.Now;
        //    wait.Until(webDriver => (DateTime.Now - timestamp) > delay);
        //}


        [OneTimeSetUp]
        public void ClearResultsDir()
        {
            AllureLifecycle.Instance.CleanupResultDirectory();
        }

    
        [Test]
        [AllureDescription("Подписание")]
        [AllureSeverity(SeverityLevel.critical)]
  
        
        public void SignatureAktViezdnogo()
        {
            
            Authorization Authorization = new Authorization(); // чтобы могли обращаться к объектам из PageHome.cs                                                  
            PageFactory.InitElements(driver, Authorization); // инициализация элементов Page Object из PageHome.cs
            MainPage MainPage = new MainPage();            
            PageFactory.InitElements(driver, MainPage);

            AllureLifecycle.Instance.WrapInStep(() =>
            {
                Authorization.Login.SendKeys("IGSN4");
                Authorization.Password.SendKeys("IGSN4");
                Authorization.Input.Click();
            }, "Авторизация");

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
              
            }
            else
            {
                Assert.Fail("Возникла ошибка при подписании");
                driver.Quit();
            }

        }

        [Test]
        [AllureDescription("Создание")]
        [AllureSeverity(SeverityLevel.critical)]

        public void CreateAktViezdnogo()
        {

            Authorization Authorization = new Authorization();                                                  
            PageFactory.InitElements(driver, Authorization); 
            MainPage MainPage = new MainPage();
            PageFactory.InitElements(driver, MainPage);

            AllureLifecycle.Instance.WrapInStep(() =>
            {
                Authorization.Login.SendKeys("IGSN4");
                Authorization.Password.SendKeys("IGSN4");
                Authorization.Input.Click();
            }, "Авторизация");

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
            MainPage.Creat_button_akt.Click();       
            MainPage.RadioButton_AktViezdnogo.Click();
            Thread.Sleep(12000);
            MainPage.Nomer.SendKeys("Auto_test");
            MainPage.Date.SendKeys("27.07.2022");
            MainPage.Mnoyu.Click();            
            MainPage.Mnoyu_spisok.Click();
            MainPage.Na_osnovanii.Click();
            MainPage.Na_osnovanii_spisok.Click();
            MainPage.Date_provedeniya.SendKeys("27.07.2022");
            MainPage.Po_resultatam_ustanovleno.SendKeys("test");
            MainPage.Zhurnal.Click();
            MainPage.Podpisivayushiy.Click();
            MainPage.Podpisivayushiy_spisok.Click(); 
            MainPage.Akt_viezdnogo_obsledovaniya_vkladka.Click();
            MainPage.Save_document.Click();
          //  Thread.Sleep(15000);
            MainPage.Perenesti_deistvie_iz_zadaniya.Click();
          

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