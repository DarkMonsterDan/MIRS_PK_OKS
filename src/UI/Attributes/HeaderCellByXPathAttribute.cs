using OpenQA.Selenium;
using System;

namespace TestFramework.UI
{
    public class HeaderCellByXPathAttribute : Attribute, IHeaderCellByAttribute
    {
        private readonly string xpath;

        public HeaderCellByXPathAttribute(string xpath)
        {
            this.xpath = xpath;
        }

        public By By => By.XPath(xpath);
    }
}
