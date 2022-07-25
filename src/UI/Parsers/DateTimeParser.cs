using System;
using System.Globalization;

namespace TestFramework.UI.Parsers
{
    public class DateTimeParser : IParser<DateTime>
    {
        public DateTime Parse(string source, string format) => DateTime.ParseExact(source, format ?? "dd.MM.yyyy HH:mm:ss", CultureInfo.InvariantCulture);
    }
}
