using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;

namespace TestFramework.UI.Attributes
{
    public class FindByAttribute : Attribute, IFindByAttribute
    {

        public FindByAttribute(By by)
        {
            By = by;
        }

        public By By { get; }

    }
}
