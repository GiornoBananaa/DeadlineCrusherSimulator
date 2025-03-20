using Core.ObjectContainer;

namespace Core.EntitySystem
{
    public abstract class ReactiveSystem<T>
    {
        private readonly ObjectContainer<T> _objectContainer;
        
        public ReactiveSystem(ObjectContainer<T> objectContainer)
        {
            _objectContainer = objectContainer;
            SubscribeToObjects();
        }
        
        public void SubscribeToObjects()
        {
            foreach (var obj in _objectContainer.Objects)
            {
                Subscribe(obj);
            }

            _objectContainer.OnObjectAdded += Subscribe;
        }
        
        public abstract void Subscribe(T obj);
    }
}