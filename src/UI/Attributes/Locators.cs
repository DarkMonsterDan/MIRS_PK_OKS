using OpenQA.Selenium;
using System.Collections.Generic;

namespace TestFramework.UI.Attributes
{
    public interface IFindByAttribute
    {
        By By { get; }
    }

    public interface IFrameByAttribute
    {
        By By { get; }
    }

    public interface IWindowAttribute
    {
        string Regex { get; }
    }

    public interface IItemByAttribute
    {
        By By { get; }
    }
}
