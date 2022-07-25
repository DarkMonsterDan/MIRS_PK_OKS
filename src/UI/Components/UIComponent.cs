using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using TestFramework.Core;
using TestFramework.Logging;
using System;
using System.Linq;

namespace TestFramework.UI
{
    public class ElementFoundEventArgs : EventArgs
    {
        public IWebDriver Driver { get; set; }
        public IWebElement Element { get; set; }
    }

    public class UIComponent : Component, IComponent
    {
        [Import] protected IApplicationPool ApplicationPool;

        public void Do(Action<Actions> action)
        {
            var a = new Actions(ApplicationPool.CurrentApplication.Driver);
            action(a);
            a.Perform();
        }

        public Actions Actions => new Actions(ApplicationPool.CurrentApplication.Driver);
    }
}
