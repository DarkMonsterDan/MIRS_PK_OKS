using TestFramework.Core;
using System;

namespace TestFramework.UI
{
    public class WebAppSettings
    {
        public string DriverType { get; set; }
        public string Url { get; set; }
        public TimeSpan SearchControlTimeout { get; set; } = TimeSpan.FromSeconds(30);
    }
}
