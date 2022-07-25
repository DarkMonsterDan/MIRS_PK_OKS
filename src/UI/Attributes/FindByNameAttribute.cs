using OpenQA.Selenium;
using TestFramework.UI.Attributes;

namespace TestFramework.UI
{
    public class FindByNameAttribute : FindByAttribute
    {
        public FindByNameAttribute(string name) : base(By.Name(name))
        {
        }
    }
}
