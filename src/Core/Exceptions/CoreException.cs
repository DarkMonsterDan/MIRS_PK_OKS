using System;
using System.Runtime.Serialization;

namespace TestFramework.Core.Exceptions
{
    public class TestFrameworkCoreException : TestFrameworkException
    {
        public TestFrameworkCoreException()
        {
        }

        public TestFrameworkCoreException(string message) : base(message)
        {
        }

        public TestFrameworkCoreException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected TestFrameworkCoreException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
