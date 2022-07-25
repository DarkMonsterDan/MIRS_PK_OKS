using OpenQA.Selenium;
using TestFramework.Core;


namespace TestFramework.UI
{
    [Name("Всплывающее сообщение")]
    public class UIAlert : UIComponent
    {
        public IWait<IAlert> WaitAlert() => new Wait<IAlert>("Alert", () => ApplicationPool.CurrentApplication.Driver.SwitchTo().Alert())
            .Timeout(ApplicationPool.CurrentApplication.SearchControlTimeout);

        [Name("Текст")]
        public string Text => WaitAlert().Until().Text;

        public void Accept() => Do(() =>
        {
            Log.Info($"{this} = Принять");
            WaitAlert().Until().Accept();
        });

        public void Dismiss() => Do(() =>
        {
            Log.Info($"{this} = Отклонить");
            WaitAlert().Until().Dismiss();
        });
    }
}
