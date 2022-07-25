using TestFramework.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace TestFramework.UI
{
    public class UILink<T> : UILabel<T>
    {
        public UILink(IParser<T> parser) : base(parser)
        {
        }

        [Name("Адрес ссылки")]
        public string Href => GetAttribute("href");
    }

    public class UILink : UILink<string>
    {
        public UILink(IParser<string> parser) : base(parser)
        {
        }
    }
}
