using OpenQA.Selenium;
using TestFramework.UI.Attributes;

namespace TestFramework.UI
{
    public class FindByIdAttribute : FindByAttribute
    {
        public FindByIdAttribute(string id) : base(By.Id(id))
        {
        }
    }
}
