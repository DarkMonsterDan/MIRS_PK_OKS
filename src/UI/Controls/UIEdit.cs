using TestFramework.Core;

namespace TestFramework.UI
{
    public interface IEdit<T> : IControl
    {
        void SendKeys(string text);
        T Value { get; }
    }

    public class UIEdit<T> : UIControl, IEdit<T>
    {
        private readonly IParser<T> parser;

        public UIEdit(IParser<T> parser)
        {
            this.parser = parser;
        }

        public void Set(T value) => Do(() =>
        {
            var element = WaitElement()
                .Until(e => e.Displayed && e.Enabled);
            element.Clear();
            element.SendKeys(value.ToString());
            Log.Info($"{this} = {Value}");
        });

        public void Set(string value) => Do(() =>
        {
            var element = WaitElement()
                .Until(e => e.Displayed && e.Enabled);
            element.Clear();
            element.SendKeys(value.ToString());
            Log.Info($"{this} = {Value}");
        });

        public void Clear() => Do(() =>
        {
            var element = WaitElement()
                .Until(e => e.Displayed && e.Enabled);
            element.Clear();
            Log.Info($"{this} = Очистить");
        });

        public void SendKeys(string text) => Do(() =>
        {
            var element = WaitElement()
                .Until(e => e.Displayed && e.Enabled);
            element.SendKeys(text);
            Log.Info($"{this} ввести \"{text}\"");
        });

        [ParentName]
        public T Value => parser.Parse(GetAttribute("value"), MetaInfo.GetAttribute<FormatAttribute>()?.Format);
    }
}
