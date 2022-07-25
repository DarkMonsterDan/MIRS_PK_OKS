namespace TestFramework.UI.Parsers
{
    public class DecimalParser : IParser<decimal>
    {
        public decimal Parse(string source, string format) => decimal.Parse(source);
    }
}
