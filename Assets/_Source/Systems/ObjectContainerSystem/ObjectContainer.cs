using System.Collections.Generic;

namespace Systems.ObjectContainerSystem
{
    public class ObjectContainer<T>
    {
        private readonly List<T> _objects = new List<T>();
        
        public IEnumerable<T> Objects => _objects;
        
        public void Add(T obj)
        {
            _objects.Add(obj);
        }
        
        public void Remove(T obj)
        {
            _objects.Remove(obj);
        }
    }
}