using System;

namespace TestFramework.Core
{
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property)]
    public class ImportAttribute : Attribute
    { }
}
