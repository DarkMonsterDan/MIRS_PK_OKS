using OpenQA.Selenium;
using TestFramework.UI.Attributes;

namespace TestFramework.UI
{
    public class FindByCssAttribute : FindByAttribute
    {
        public FindByCssAttribute(string cssSelector) : base(By.CssSelector(cssSelector))
        {
        }
    }

    public class FindByLabel : FindByXPathAttribute
    {
        public FindByLabel(string label) : base($"//*[label={label}]")
        {
        }
    }
}
