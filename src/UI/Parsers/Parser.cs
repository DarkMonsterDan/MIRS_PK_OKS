namespace TestFramework.UI
{
    public interface IParser<T>
    {
        T Parse(string source, string format);
    }
}
