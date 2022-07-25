using System;
using System.Runtime.Serialization;

namespace TestFramework.Core.Exceptions
{
    public class RequiredAttributeNotFoundException : TestFrameworkCoreException
    {
        public RequiredAttributeNotFoundException()
        {
        }

        public RequiredAttributeNotFoundException(string message) : base(message)
        {
        }

        public RequiredAttributeNotFoundException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected RequiredAttributeNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
