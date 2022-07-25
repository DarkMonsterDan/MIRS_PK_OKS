using System;
using System.Runtime.Serialization;

namespace TestFramework.Core.Exceptions
{
    public class ComponentNotInitializedException : TestFrameworkCoreException
    {
        public ComponentNotInitializedException()
        {
        }

        public ComponentNotInitializedException(string message) : base(message)
        {
        }

        public ComponentNotInitializedException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected ComponentNotInitializedException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
