using OpenQA.Selenium;
using TestFramework.UI.Attributes;

namespace TestFramework.UI
{
    public class FrameByIdAttribute : FrameByAttribute
    {
        public FrameByIdAttribute(string id) : base(By.Id(id))
        {
        }
    }
}
