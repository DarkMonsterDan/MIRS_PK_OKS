using System;

namespace TestFramework.Logging
{
    public class Unsubscriber : IDisposable
    {
        private readonly Action unsubscribe;

        public Unsubscriber(Action unsubscribe)
        {
            this.unsubscribe = unsubscribe;
        }

        public void Dispose() => unsubscribe?.Invoke();
    }
}
