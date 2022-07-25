using System;

namespace TestFramework.Core
{
    /// <summary>
    /// Описание компонента
    /// </summary>
    public class NameAttribute : Attribute
    {
        public NameAttribute(string name)
        {
            Name = name;
        }

        public string Name { get; }
    }

    public class ParentNameAttribute : Attribute
    { }
}
