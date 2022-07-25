using OpenQA.Selenium;
using TestFramework.Core;
using TestFramework.UI.Attributes;
using System.Collections.Generic;
using System.Linq;
using System;

namespace TestFramework.UI
{
    public class UIList<TComponent> : UIControl
        where TComponent : IComponent
    {
        private readonly IFactory<TComponent> itemFactory;

        public UIList(IFactory<TComponent> itemFactory)
        {
            this.itemFactory = itemFactory;
        }

        public IComponentCollection<TComponent> Items => new ComponentCollection<TComponent>(FindItems().Select(item => itemFactory.Create(item)), ToString());

        protected IReadOnlyCollection<IWebElement> FindItems()
        {
            var element = WaitElement().Until(x => x.Displayed);
            return element.FindElements(MetaInfo.GetRequiredAttribute<IItemByAttribute>().By);
        }

        public string Value => GetValue();

        private string GetValue()
        {
            int vId;
            if (Int32.TryParse(GetAttribute("value"), out vId))
                return FindItems().FirstOrDefault(x => x.GetAttribute("value") == vId.ToString()).Text;
            throw new Exception("Значение не обнаружено в списке элементов");
        }
    }
}
