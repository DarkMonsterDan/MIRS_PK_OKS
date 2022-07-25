using System.Collections.Generic;

namespace TestFramework.Core
{
    public class ExpressionSettings
    {
        public string MemberSeparator { get; set; } = " -> ";
        public Dictionary<string, string> Replaces { get; set; } = new Dictionary<string, string>() {
            { " OrElse ", " ИЛИ " },
            { " Or ", " ИЛИ " },
            { " AndAlso ", " И " },
            { " And ", " И " }
        };
    }
}
