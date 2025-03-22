using Core.EntitySystem;
using Core.Factory;
using Core.ObjectContainer;
using UnityEngine;

namespace GameFeatures.TowerDefence
{
    public class TaskHealthStateReactSystem : ReactiveSystem<Task>
    {
        private readonly IPoolFactory<Task> _poolFactory;
        
        public TaskHealthStateReactSystem(ObjectContainer<Task> objectContainer, IPoolFactory<Task> poolFactory) : base(objectContainer)
        {
            _poolFactory = poolFactory;
        }

        public override void Subscribe(Task obj)
        {
            obj.OnHealthChanged += OnHealthChanged;
        }

        public override void Unsubscribe(Task obj)
        {
            obj.OnHealthChanged -= OnHealthChanged;
        }

        private void OnHealthChanged(Task task, float health)
        {
            if(health <= 0)
                _poolFactory.Release(task);
        }
    }
}