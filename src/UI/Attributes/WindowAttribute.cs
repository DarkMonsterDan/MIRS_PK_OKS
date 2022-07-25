using TestFramework.UI.Attributes;
using System;

namespace TestFramework.UI
{
    public class WindowAttribute : Attribute, IWindowAttribute
    { 
        public WindowAttribute(string pattern)
        {
            Regex = pattern;
        }

        public string Regex { get; }
    }
}
