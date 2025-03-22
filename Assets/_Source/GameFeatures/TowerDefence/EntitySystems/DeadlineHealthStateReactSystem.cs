using Core.Factory;
using Core.ObjectContainer;
using Core.EntitySystem;

namespace GameFeatures.TowerDefence
{
    public class DeadlineHealthStateReactSystem : ReactiveSystem<Deadline>
    {
        private readonly IPoolFactory<Deadline> _poolFactory;
        
        public DeadlineHealthStateReactSystem(ObjectContainer<Deadline> objectContainer, IPoolFactory<Deadline> poolFactory) : base(objectContainer)
        {
            _poolFactory = poolFactory;
        }

        public override void Subscribe(Deadline obj)
        {
            obj.OnHealthChanged += OnHealthChanged;
        }

        public override void Unsubscribe(Deadline obj)
        {
            obj.OnHealthChanged -= OnHealthChanged;
        }
        
        private void OnHealthChanged(Deadline deadline, float health)
        {
            if(health <= 0)
                _poolFactory.Release(deadline);
        }
    }
}