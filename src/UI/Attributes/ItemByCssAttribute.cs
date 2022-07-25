using OpenQA.Selenium;
using TestFramework.UI.Attributes;
using System;

namespace TestFramework.UI
{
    public class ItemByCssAttribute : Attribute, IItemByAttribute
    {
        private readonly string css;

        public ItemByCssAttribute(string css)
        {
            this.css = css;
        }

        public By By => By.CssSelector(css);
    }
}
