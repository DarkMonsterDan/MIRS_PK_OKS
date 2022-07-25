using TestFramework.Core;

namespace TestFramework.UI
{
    public class UICheckBox : UIControl
    {
        public void Set(bool value) => Do(() =>
        {
            var element = WaitElement().Until(x => x.Displayed && x.Enabled);
            if (element.Selected != value)
                element.Click();
            Log.Info($"{this} = {value}");
        });
    }
}
