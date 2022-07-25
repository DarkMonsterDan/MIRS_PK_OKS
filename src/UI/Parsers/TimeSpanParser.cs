using System;

namespace TestFramework.UI.Parsers
{
    public class TimeSpanParser : IParser<TimeSpan>
    {
        public TimeSpan Parse(string source, string format) => TimeSpan.Parse(source);
    }
}
