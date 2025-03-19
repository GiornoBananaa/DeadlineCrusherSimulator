using System.Collections.Generic;
using Core.ObjectContainer;

namespace Core.ServiceUpdater
{
    public abstract class ExecuteSystem<T> : IExecutable
    {
        private readonly ObjectContainer<T> _objectContainer;
        
        public ExecuteSystem(ObjectContainer<T> objectContainer, ServiceUpdater serviceUpdater)
        {
            serviceUpdater.Subscribe(this);
            _objectContainer = objectContainer;
        }
        
        public void Execute()
        {
            Execute(_objectContainer.Objects);
        }

        protected abstract void Execute(IEnumerable<T> objects);
    }
}