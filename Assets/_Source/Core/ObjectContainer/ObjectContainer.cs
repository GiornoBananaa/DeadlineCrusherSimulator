using System;
using System.Collections.Generic;

namespace Core.ObjectContainer
{
    public class ObjectContainer<T>
    {
        private readonly List<T> _objects = new List<T>();
        
        public IEnumerable<T> Objects => _objects;
        
        public event Action<T> OnObjectAdded;
        public event Action<T> OnObjectRemoved;
        
        public void Add(T obj)
        {
            _objects.Add(obj);
            OnObjectAdded?.Invoke(obj);
        }
        
        public void Remove(T obj)
        {
            _objects.Remove(obj);
            OnObjectRemoved?.Invoke(obj);
        }
    }
}