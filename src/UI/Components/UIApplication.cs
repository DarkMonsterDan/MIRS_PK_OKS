using OpenQA.Selenium;
using System;
using System.Text.RegularExpressions;

namespace TestFramework.UI
{
    public interface IApplication
    {
        IWebDriver Driver { get; }
        IWebElement CurrentFrame { get; }
        string CurrentWindowHandle { get; }
        TimeSpan SearchControlTimeout { get; }
        IWebDriver SwitchToWindow(string regex);
        IWebDriver SwitchToFrame(IWebElement frameElement);
        IWebDriver SwitchToDefaultContent();
        IWebDriver SwitchToDefaultWindow();
        IDisposable Load();
        void Close();
    }

    public abstract class UIApplication : UIComponent, IApplication, IDisposable
    {
        public UIApplication()
        {
        }

        public UIApplication(TimeSpan searchControlTimeout)
        {
            SearchControlTimeout = searchControlTimeout;
        }

        private IWebDriver driver;
        string defaultWindowHandle;

        public IWebDriver Driver => driver ?? throw new Exception($"У приложения {this} не загружен Driver");

        public IWebElement CurrentFrame { get; private set; }

        public string CurrentWindowHandle { get; private set; }

        public TimeSpan SearchControlTimeout { get; } = TimeSpan.FromSeconds(30);

        public void Close() => Do(() =>
        {
            if (driver == null)
                return;
            driver?.Quit();
            driver = null;
            ApplicationPool.Unload(this);
            Log.Info($"Закрыто приложение: {this}");
        });

        public abstract IWebDriver CreateDriver();

        public IDisposable Load() => Do(() =>
        {
            driver = CreateDriver();
            defaultWindowHandle = driver.CurrentWindowHandle;
            CurrentWindowHandle = defaultWindowHandle;
            var disposer = ApplicationPool.Load(this);
            Log.Info($"Загружено приложение: {this}");
            return disposer;
        });

        public IWebDriver SwitchToWindow(string regex)
        {
            foreach(var handle in Driver.WindowHandles)
            {
                Driver.SwitchTo().Window(handle);
                if(Regex.IsMatch(Driver.Title, regex))
                {
                    CurrentWindowHandle = handle;
                    return Driver;
                }
            }

            throw new Exception($"Не удалось найти окно по шаблону: \"{regex}\"");
        }

        public IWebDriver SwitchToFrame(IWebElement frameElement)
        {
            try
            {
                Driver.SwitchTo().Frame(frameElement);
                CurrentFrame = frameElement;
                return Driver;
            }
            catch
            {
                return Driver;
            }            
        }

        public IWebDriver SwitchToDefaultWindow()
        {
            if(CurrentWindowHandle != defaultWindowHandle)
            {
                Driver.SwitchTo().Window(defaultWindowHandle);
                CurrentWindowHandle = defaultWindowHandle;
            }

            return Driver;
        }

        public IWebDriver SwitchToDefaultContent()
        {
            if (CurrentFrame != null)
            {
                Driver.SwitchTo().DefaultContent();
                CurrentFrame = null;
            }
            return Driver;
        }

        public void Dispose()
        {
            Close();
        }
    }
}
