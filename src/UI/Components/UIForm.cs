using System.Collections.ObjectModel;
using OpenQA.Selenium;
using TestFramework.Core;
using TestFramework.UI.Attributes;

namespace TestFramework.UI
{


    public class UIForm : UIComponent
    {
        string windowHandle = null;
        IWebElement frame = null;

        IWebDriver SwitchToWindow(IApplication application)
        {
            var driver = application.Driver;
            var windowAttribute = MetaInfo.GetAttribute<IWindowAttribute>();
            if (windowAttribute == null)
                return application.SwitchToDefaultWindow();                

            if (windowHandle == application.CurrentWindowHandle)
                return driver;

            application.SwitchToWindow(windowAttribute.Regex);
            windowHandle = application.CurrentWindowHandle;
            return application.Driver;
        }

        IWebDriver SwitchToFrame(IApplication application)
        {
            var driver = application.Driver;
            var frameByAttribute = MetaInfo.GetAttribute<IFrameByAttribute>();
            if (frameByAttribute == null)
            {
                return application.SwitchToDefaultContent();
            }                

            if (frame == application.CurrentFrame && frame != null)
                return driver;

            driver = application.SwitchToDefaultContent();
            frame = driver.FindElement(frameByAttribute.By);
            application.SwitchToFrame(frame);
            return driver;
        }

        ISearchContext FindElement(IWebDriver driver)
        {
            var findByByAttribute = MetaInfo.GetAttribute<IFindByAttribute>();
            if (findByByAttribute == null)
            {
                return driver;
            }

            return driver.FindElement(findByByAttribute.By);
        }

        ISearchContext GetSearchContext()
        {
            var application = ApplicationPool.CurrentApplication;
            var driver = SwitchToWindow(application);
            driver = SwitchToFrame(application);
            return FindElement(driver);
        }

        public IWebElement FindElement(By by)
        {
            //Todo StaleReferenceException сделать сброс windowHandle и frame
            return GetSearchContext().FindElement(by);
        }

        public ReadOnlyCollection<IWebElement> FindElements(By by)
        {
            return GetSearchContext().FindElements(by);
        }
    }

}
