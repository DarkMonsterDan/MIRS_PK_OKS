using OpenQA.Selenium;
using TestFramework.UI.Attributes;

namespace TestFramework.UI
{
    public class FrameByNameAttribute : FrameByAttribute
    {
        public FrameByNameAttribute(string name) : base(By.Name(name))
        {
        }
    }
}
