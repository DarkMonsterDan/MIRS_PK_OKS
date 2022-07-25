using OpenQA.Selenium;
using TestFramework.UI.Attributes;

namespace TestFramework.UI
{
    public class FindByXPathAttribute : FindByAttribute
    {
        public FindByXPathAttribute(string xPath) : base(By.XPath(xPath))
        {
        }
    }
}
