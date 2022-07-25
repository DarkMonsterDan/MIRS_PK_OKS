using TestFramework.Core;
using System;

namespace TestFramework.UI
{
    public interface IColumnAttribute
    {
        int Index { get; }
        string Regex { get; }
    }

    public class ColumnAttribute : Attribute, IColumnAttribute
    {
        public ColumnAttribute(int index)
        {
            Index = index;
        }

        public ColumnAttribute(string regex)
        {
            Regex = regex;
        }

        public int Index { get; } = -1;
        public string Regex { get; }
    }

    public class NamedColumnAttribute : NameAttribute, IColumnAttribute
    {
        public NamedColumnAttribute(string name) : base(name)
        {
            Regex = name;
        }

        public int Index => -1;

        public string Regex { get; }
    }
}
