using Core.Factory;
using Core.ObjectContainer;
using Core.EntitySystem;
using GameFeatures.Menu;

namespace GameFeatures.TowerDefence
{
    public class DeadlineHealthStateReactSystem : ReactiveSystem<Deadline>
    {
        private readonly IPoolFactory<Deadline> _poolFactory;
        private readonly PlayerScore.PlayerScore _playerScore;
        
        public DeadlineHealthStateReactSystem(ObjectContainer<Deadline> objectContainer, IPoolFactory<Deadline> poolFactory, 
            PlayerScore.PlayerScore playerScore) : base(objectContainer)
        {
            _poolFactory = poolFactory;
            _playerScore = playerScore;
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
            {
                _poolFactory.Release(deadline);
                _playerScore.AddScore();
            }
        }
    }
}