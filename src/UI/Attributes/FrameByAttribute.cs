using OpenQA.Selenium;
using System;

namespace TestFramework.UI.Attributes
{
    public abstract class FrameByAttribute : Attribute, IFrameByAttribute
    {
        public FrameByAttribute(By by)
        {
            By = by;
        }

        public By By { get; }
    }
}
