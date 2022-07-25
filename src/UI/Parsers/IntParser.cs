namespace TestFramework.UI.Parsers
{
    public class IntParser : IParser<int>
    {
        public int Parse(string source, string format) => int.Parse(source);
    }
}
