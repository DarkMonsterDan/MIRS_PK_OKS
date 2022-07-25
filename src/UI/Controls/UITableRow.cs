using OpenQA.Selenium;
using TestFramework.Core;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using TestFramework.UI.Attributes;

namespace TestFramework.UI
{
    public interface IUITableRow : IControl
    {
        IEnumerable<ITableColumn> Map(IEnumerable<ITableColumn> columns, IWebElement row);
    }

    public class UITableRow : UIControl, IUITableRow
    {
        public virtual IEnumerable<ITableColumn> Map(IEnumerable<ITableColumn> columns, IWebElement row)
        {
            MetaInfo.Parent = row;
            var by = MetaInfo.GetRequiredAttribute<IItemByAttribute>().By;
            var cells = row.FindElements(by);
            foreach(var child in MetaInfo.Children)
            {
                var columnAttribute = child.MetaInfo.GetAttribute<IColumnAttribute>();
                if (columnAttribute == null)
                    continue;
                var index = columnAttribute.Index > -1 ? columnAttribute.Index : GetIndex(columns, columnAttribute);
                if(-1 < index && index < cells.Count)
                {
                    var cell = cells[index];
                    child.MetaInfo.Parent = cell;
                }
            }
            return columns;
        }

        protected int GetIndex(IEnumerable<ITableColumn> columns, IColumnAttribute columnAttribute) 
            => columns.FirstOrDefault(x => Regex.IsMatch(x.Name, columnAttribute.Regex))?.Index ?? -1;
    }
}
