using System;
using System.Runtime.Serialization;

namespace TestFramework.Core.Exceptions
{
    public class TestFrameworkException : Exception
    {
        public TestFrameworkException()
        {
        }

        public TestFrameworkException(string message) : base(message)
        {
        }

        public TestFrameworkException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected TestFrameworkException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
