using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using TestFramework.Core;
using TestFramework.UI.Attributes;
using System.Collections.ObjectModel;
using System.Linq;

namespace TestFramework.UI
{
    public interface IControl : IComponent, ISearchContext
    {
        void Click();

        [Name("Текст")]
        string Text { get; }

        [Name("Атрибут \"{attributeName}\"")]
        string GetAttribute(string attributeName);

        [Name("Выбран")]
        bool Selected { get; }

        IWebElement FindElement();
    }

    public class UIControl : UIComponent, IControl
    {
        public IWait<IWebElement> WaitElement() => new Wait<IWebElement>($"Поиск контрола \"{this}\"", FindElement)
            .Timeout(ApplicationPool.CurrentApplication.SearchControlTimeout);

        IWebElement frame = null;

        protected IWebDriver Driver => ApplicationPool.CurrentApplication.Driver;

        protected ISearchContext SwitchToFrame(ISearchContext searchContext)
        {
            var application = ApplicationPool.CurrentApplication;            
            var driver = application.Driver;
            var frameByAttribute = MetaInfo.GetAttribute<IFrameByAttribute>();
            if (frameByAttribute == null)
                return searchContext;

            if (frame == application.CurrentFrame && frame != null)
                return searchContext;

            frame = searchContext.FindElement(frameByAttribute.By);
            application.SwitchToFrame(frame);
            return driver;
        }

        ISearchContext ParentSearchContext => MetaInfo.GetBranch().OfType<ISearchContext>().FirstOrDefault() ?? ApplicationPool.CurrentApplication.Driver;

        public IWebElement FindElement()
        {
            var searchContext = ParentSearchContext;
            searchContext = SwitchToFrame(searchContext);
            var shadowRootAttribute = MetaInfo.GetAttribute<IShadowRootAttribute>();
            if(shadowRootAttribute != null)
            {
                var shadowRoot = searchContext.FindElement(shadowRootAttribute.By);
                searchContext = (Driver as IJavaScriptExecutor)?.ExecuteScript("return arguments[0].shadowRoot;", shadowRoot) as IWebElement ?? searchContext;
            }

            IFindByAttribute findByAttribute;
            if (searchContext is IWebElement parentElement)
            {
                findByAttribute = MetaInfo.GetAttribute<IFindByAttribute>();
                if (findByAttribute == null)
                    return parentElement;
                return parentElement.FindElement(findByAttribute.By);
            }
            findByAttribute = MetaInfo.GetRequiredAttribute<IFindByAttribute>();
            var element = searchContext.FindElement(findByAttribute.By);
            return element;
        }

        public IWebElement FindElement(By by)
        {
            return FindElement().FindElement(by);
        }

        public ReadOnlyCollection<IWebElement> FindElements(By by)
        {
            return FindElement().FindElements(by);
        }

        public void Click() => Do(() =>
        {
            var element = WaitElement().Until(e => e.Displayed);
            element.Click();
            Log.Info($"{this} = Click");
        });

        public void DoubleClick() => Do(() =>
        {
            var element = WaitElement().Until(e => e.Displayed);
            new Actions(Driver).DoubleClick().Perform();
            Log.Info($"{this} = Double Click");
        });

        public void RightClick() => Do(() =>
        {
            var element = WaitElement().Until(e => e.Displayed);
            new Actions(Driver).ContextClick().Perform();
            Log.Info($"{this} = Right Click");
        });

        [Name("Атрибут \"{attributeName}\"")]
        public string GetAttribute(string attributeName) => WaitElement().Until().GetAttribute(attributeName);

        [Name("Текст")]
        public string Text => WaitElement().Until().Text;

        [Name("Выбран")]
        public bool Selected => WaitElement().Until().Selected;

        [Name("Отображается")]
        public bool Displayed
        {
            get
            {
                try
                {
                    return FindElement().Displayed;
                }
                catch
                {
                    return false;
                }
            }
        }

        [Name("Активен")]
        public bool Enabled
        {
            get
            {
                try
                {
                    return FindElement().Enabled;
                }
                catch
                {
                    return false;
                }
            }
        }

        [Name("Существует")]
        public bool Exists
        {
            get
            {
                try
                {
                    FindElement();
                    return true;
                }
                catch
                {
                    return false;
                }
            }
        }
    }
}
