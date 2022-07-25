using OpenQA.Selenium;
using TestFramework.UI.Attributes;
using System;

namespace TestFramework.UI
{
    public class ItemByXPathAttribute : Attribute, IItemByAttribute
    {
        private readonly string xpath;

        public ItemByXPathAttribute(string xpath)
        {
            this.xpath = xpath;
        }

        public By By => By.XPath(xpath);
    }
}
