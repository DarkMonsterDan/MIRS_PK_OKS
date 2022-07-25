using System;
using System.Collections.Generic;
using System.Linq;

namespace TestFramework.UI
{
    public interface IApplicationPool
    {
        IApplication CurrentApplication { get; }
        IDisposable Load(IApplication application);
        void Unload(IApplication application);
    }

    public class ApplicationPool :IApplicationPool
    {
        private readonly List<IApplication> applications = new List<IApplication>();

        public IApplication CurrentApplication => applications.LastOrDefault() ?? throw new Exception("Ни одно приложение не было загружено в ApplicationPool");
        
        public IDisposable Load(IApplication application)
        {
            applications.Add(application);
            return new ApplicationUnloader(application);
        }

        public void Unload(IApplication application)
        {
            applications.Remove(application);
        }
    }

    public class ApplicationUnloader : IDisposable
    {
        private readonly IApplication application;

        public ApplicationUnloader(IApplication application)
        {
            this.application = application;
        }

        public void Dispose()
        {
            application.Close();
        }
    }
}
