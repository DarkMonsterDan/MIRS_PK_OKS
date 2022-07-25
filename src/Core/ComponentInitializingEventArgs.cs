using System;

namespace TestFramework.Core
{
    public class ComponentInitializingEventArgs : EventArgs
    {

        public ComponentInitializingEventArgs(IComponent component, IMetaInfo metaInfo)
        {
            Component = component;
            MetaInfo = metaInfo;
        }

        public IComponent Component { get; }
        public IMetaInfo MetaInfo { get; }
    }
}
