using System.Collections.Generic;
using Core.ObjectContainer;

namespace Core.EntitySystem
{
    public abstract class ExecuteSystem<T> : IExecutable
    {
        private readonly ObjectContainer<T> _objectContainer;
        
        protected virtual UpdateMode ExecuteMode => UpdateMode.Default;
        
        public ExecuteSystem(ObjectContainer<T> objectContainer, ServiceUpdater serviceUpdater)
        {
            serviceUpdater.Subscribe(this, ExecuteMode);
            _objectContainer = objectContainer;
        }
        
        public void Execute()
        {
            Execute(_objectContainer.Objects);
        }

        protected abstract void Execute(IEnumerable<T> objects);
    }
}