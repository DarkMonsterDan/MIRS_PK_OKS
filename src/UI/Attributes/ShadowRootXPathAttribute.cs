using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Text;

namespace TestFramework.UI
{
    public interface IShadowRootAttribute
    {
        By By { get; }
    }

    public class ShadowRootXPathAttribute : Attribute, IShadowRootAttribute
    {
        private readonly string xpath;

        public ShadowRootXPathAttribute(string xpath)
        {
            this.xpath = xpath;
        }

        public By By => By.XPath(xpath);
    }
}
