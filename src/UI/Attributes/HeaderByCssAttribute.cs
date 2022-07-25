using OpenQA.Selenium;
using System;

namespace TestFramework.UI
{
    public class HeaderByCssAttribute : Attribute, IHeaderByAttribute
    {
        private readonly string css;

        public HeaderByCssAttribute(string css)
        {
            this.css = css;
        }

        public By By => By.CssSelector(css);
    }
}
