using OpenQA.Selenium;
using TestFramework.Core;
using TestFramework.Logging;
using TestFramework.UI.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace TestFramework.UI
{
    public interface IUISelectBox : IControl
    {
        void Set(params string[] regexPatterns);
        string Value { get; }
        IComponentCollection<string> Items { get; }
    }

    [ItemByXPath("./option")]
    public class UISelectBox : UIControl, IUISelectBox
    {
        public void Set(params string[] regexPatterns) => Do(() =>
        {
            var multiSelect = MetaInfo.GetAttribute<MultiSelectAttribute>() == null;
            var items = FindItems(WaitElement().Until(x => x.Displayed && x.Enabled));
            var foundItems = new List<IWebElement>();
            foreach (var regex in regexPatterns)
            {
                foundItems.AddRange(items.Where(x => Regex.IsMatch(x.Text, regex)));
            }

            if (multiSelect)
            {
                foreach (var item in foundItems)
                    SetSelected(item, Log);
            }
            else
            {
                var item = foundItems.FirstOrDefault();
                if (item == null)
                    throw new Exception($"Не удалось найти элемент по шаблону:  { string.Join(", ", regexPatterns.Select(x => $"\"{x}\""))}");
                SetSelected(item, Log);
            }
        });

        public IComponentCollection<string> Items => new ComponentCollection<string>(FindItems(WaitElement().Until()).Select(x => x.Text), $"{this} -> Элементы");

        void SetSelected(IWebElement element, ILogService log)
        {
            if (!element.Selected)
                element.Click();
            log.Info($"{this} = {element.Text}");
        }

        protected IReadOnlyCollection<IWebElement> FindItems(IWebElement selectBox)
        {
            return selectBox.FindElements(MetaInfo.GetRequiredAttribute<IItemByAttribute>().By);
        }

        public string Value => GetValue();

        private string GetValue()
        {
            int vId;
            if (Int32.TryParse(GetAttribute("value"), out vId))
                return FindItems(WaitElement().Until()).FirstOrDefault(x => x.GetAttribute("value") == vId.ToString()).Text;
            throw new System.Exception("Значение не обнаружено в списке элементов");
        }
    }
}
