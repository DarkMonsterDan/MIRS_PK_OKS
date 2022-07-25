using OpenQA.Selenium;

using SeleniumExtras.PageObjects;

namespace MIRSPKOKS.PagesQA
{
    class Authorization
    {
      
        [FindsBy(How = How.XPath, Using = "//input[@id = 'login-inputEl']")]
        public IWebElement Login { get; set; }
     
        [FindsBy(How = How.XPath, Using = "//input[@id = 'password-inputEl']")]
        public IWebElement Password { get; set; }

        [FindsBy(How = How.Id, Using = "button-1017-btnWrap")]
        public IWebElement Input { get; set; }

        


    }

}
