using Core.Factory;
using Core.ObjectContainer;
using Core.EntitySystem;

namespace GameFeatures.TowerDefence
{
    public class DeadlineHealthStateReactSystem : ReactiveSystem<Deadline>
    {
        private readonly PoolFactory<Deadline> _poolFactory;
        
        public DeadlineHealthStateReactSystem(ObjectContainer<Deadline> objectContainer, PoolFactory<Deadline> poolFactory) : base(objectContainer)
        {
            _poolFactory = poolFactory;
        }

        public override void Subscribe(Deadline obj)
        {
            obj.OnHealthChanged += (health) => OnHealthChanged(obj, health);
        }
        
        private void OnHealthChanged(Deadline deadline, float health)
        {
            if(health <= 0)
                _poolFactory.Release(deadline);
        }
    }
}