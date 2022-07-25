using System;

namespace TestFramework.UI
{
    public class FormatAttribute : Attribute
    {
        public FormatAttribute(string format)
        {
            Format = format;
        }

        public string Format { get; }
    }
}
