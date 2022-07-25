using TestFramework.Core;

namespace TestFramework.UI
{
    [Format(null)]
    public class UILabel<T> : UIControl
    {
        private readonly IParser<T> parser;

        public UILabel(IParser<T> parser)
        {
            this.parser = parser;
        }

        [Name("Значение")]
        public T Value => parser.Parse(Text, MetaInfo.GetRequiredAttribute<FormatAttribute>().Format);
    }

    public class UILabel : UILabel<string>
    {
        public UILabel(IParser<string> parser) : base(parser)
        {
        }
    }
}
