namespace TestFramework.UI.Parsers
{
    public class DoubleParser : IParser<double>
    {
        public double Parse(string source, string format) => double.Parse(source);
    }
}
