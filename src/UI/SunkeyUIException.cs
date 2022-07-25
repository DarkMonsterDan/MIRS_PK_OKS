using TestFramework.Core.Exceptions;
using System;
using System.Runtime.Serialization;

namespace TestFramework.UI
{
    public class Exception : TestFrameworkException
    {
        public Exception()
        {
        }

        public Exception(string message) : base(message)
        {
        }

        public Exception(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected Exception(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
