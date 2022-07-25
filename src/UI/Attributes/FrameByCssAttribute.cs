using OpenQA.Selenium;
using TestFramework.UI.Attributes;

namespace TestFramework.UI
{
    public class FrameByCssAttribute : FrameByAttribute
    {
        public FrameByCssAttribute(string cssSelector) : base(By.CssSelector(cssSelector))
        {
        }
    }
}
