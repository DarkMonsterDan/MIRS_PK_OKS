using OpenQA.Selenium;
using System;

namespace TestFramework.UI
{
    public class HeaderByXPathAttribute : Attribute, IHeaderByAttribute
    {
        private readonly string xpath;

        public HeaderByXPathAttribute(string xpath)
        {
            this.xpath = xpath;
        }

        public By By => By.XPath(xpath);
    }
}
