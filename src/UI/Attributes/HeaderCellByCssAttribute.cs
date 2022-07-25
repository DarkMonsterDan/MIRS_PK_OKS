using OpenQA.Selenium;
using System;

namespace TestFramework.UI
{
    public class HeaderCellByCssAttribute : Attribute, IHeaderCellByAttribute
    {
        private readonly string css;

        public HeaderCellByCssAttribute(string css)
        {
            this.css = css;
        }

        public By By => By.CssSelector(css);
    }
}
