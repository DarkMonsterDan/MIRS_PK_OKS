namespace TestFramework.UI
{

    public interface ITableColumn
    {
        int Index { get; }
        string Name { get; }
    }

    public class TableColumn : ITableColumn
    {
        public TableColumn(int index, string name)
        {
            Index = index;
            Name = name;
        }

        public int Index { get; }
        public string Name { get; }
    }
}
