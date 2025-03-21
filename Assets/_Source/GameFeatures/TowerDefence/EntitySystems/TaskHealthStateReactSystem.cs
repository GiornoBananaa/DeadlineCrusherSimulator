using Core.EntitySystem;
using Core.Factory;
using Core.ObjectContainer;

namespace GameFeatures.TowerDefence
{
    public class TaskHealthStateReactSystem : ReactiveSystem<Task>
    {
        private readonly PoolFactory<Task> _poolFactory;
        
        public TaskHealthStateReactSystem(ObjectContainer<Task> objectContainer, PoolFactory<Task> poolFactory) : base(objectContainer)
        {
            _poolFactory = poolFactory;
        }

        public override void Subscribe(Task obj)
        {
            obj.OnHealthChanged += (health) => OnHealthChanged(obj, health);
        }
        
        private void OnHealthChanged(Task task, float health)
        {
            if(health <= 0)
                _poolFactory.Release(task);
        }
    }
}