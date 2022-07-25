using OpenQA.Selenium;
using TestFramework.UI.Attributes;

namespace TestFramework.UI
{
    public class FrameByXPathAttribute : FrameByAttribute
    {
        public FrameByXPathAttribute(string xPath) : base(By.XPath(xPath))
        {
        }
    }
}
