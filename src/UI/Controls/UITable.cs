using OpenQA.Selenium;
using TestFramework.Core;
using TestFramework.UI.Attributes;
using System.Collections.Generic;
using System.Linq;

namespace TestFramework.UI
{
    public interface IUITable<TRow> : IControl
    {
        IEnumerable<ITableColumn> Columns { get; }
        IComponentCollection<TRow> Rows { get; }
    }

    public interface IUITable<TRow, THeader> : IUITable<TRow>
    {
        THeader Header { get; set; }
    }

    public class UITable<TRow> : UIControl, IUITable<TRow>
        where TRow : IUITableRow
    {
        private readonly IFactory<TRow> rowFactory;

        public UITable(IFactory<TRow> rowFactory)
        {
            this.rowFactory = rowFactory;
        }

        public IEnumerable<ITableColumn> Columns => GetColumns(WaitElement().Until(x => x.Displayed));

        protected virtual IEnumerable<ITableColumn> GetColumns(IWebElement table)
        {
            var header = FindHeader(table);
            var cells = FindHeaderCells(header);
            return cells.Select((cell, index) => new TableColumn(index, cell.Text)).ToList();
        }

        protected virtual IEnumerable<IWebElement> FindHeaderCells(IWebElement header)
        {
            var headerCellAttribute = MetaInfo.GetAttribute<IHeaderCellByAttribute>();
            if (header == null || headerCellAttribute == null)
                return new List<IWebElement>();

            return header.FindElements(headerCellAttribute.By);
        }

        protected virtual IWebElement FindHeader(IWebElement table)
        {
            var headerAttribute = MetaInfo.GetAttribute<IHeaderByAttribute>();
            return headerAttribute == null ? null : table.FindElement(headerAttribute.By);
        }

        protected virtual IEnumerable<IWebElement> FindRowItems(IWebElement table)
        {
            return table.FindElements(MetaInfo.GetRequiredAttribute<IItemByAttribute>().By);
        }

        protected virtual IEnumerable<TRow> GetRows(IWebElement table)
        {
            var columns = GetColumns(table);
            var rowItems = FindRowItems(table);
            foreach(var rowItem in rowItems)
            {
                var row = rowFactory.Create();
                row.Map(columns, rowItem);
                yield return row;
            }
        }

        [ParentName]
        public IComponentCollection<TRow> Rows => new ComponentCollection<TRow>(GetRows(WaitElement().Until(x => x.Displayed)), $"{this} -> Строки");
    }

    public class UITable<TRow, THeader> : UITable<TRow>, IUITable<TRow, THeader>
        where THeader : IUITableRow
        where TRow : IUITableRow
    {
        public UITable(IFactory<TRow> rowFactory) : base(rowFactory)
        {
        }

        protected override IWebElement FindHeader(IWebElement table)
        {
            return Header.FindElement();
        }

        protected override IEnumerable<ITableColumn> GetColumns(IWebElement table)
        {
            var header = base.FindHeader(table);
            if(header != null)
            {
                Header.MetaInfo.Parent = header;
            }
                
            var columns = base.GetColumns(table);
            return Header.Map(columns, header ?? table);
        }

        [Name("Заголовок")]
        public THeader Header { get; set; }

        protected override void OnInitialize()
        {
            base.OnInitialize();
            var headerAttribute = MetaInfo.GetAttribute<IHeaderByAttribute>();
            if (headerAttribute != null)
                Header.MetaInfo.TypeAttributes.Add(new FindByAttribute(headerAttribute.By));
        }
    }
}
