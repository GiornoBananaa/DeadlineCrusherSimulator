using System.Linq;
using Core.DataLoading;
using Core.EntitySystem;
using Core.Factory;
using Core.ObjectContainer;
using Core.PhysicsDetection;
using GameFeatures.TowerDefence.Configs;
using GameFeatures.WorkProgress;
using UnityEngine;

namespace GameFeatures.TowerDefence
{
    public class DeadlineExpireSystem : ReactiveSystem<Deadline>, ITriggerEnterListener
    {
        private readonly ExpirationCounter _expirationCounter;
        private readonly IPoolFactory<Deadline> _poolFactory;
        private readonly LayerMask _expirationLineLayerMask;
        
        public DeadlineExpireSystem(ObjectContainer<Deadline> objectContainer, IRepository<ScriptableObject> dataRepository, 
            ExpirationCounter expirationCounter, IPoolFactory<Deadline> poolFactory) : base(objectContainer)
        {
            DeadlinesConfig config = dataRepository.GetItem<DeadlinesConfig>().FirstOrDefault();
            if(config != null)
                _expirationLineLayerMask = config.ExpirationLineLayerMask;

            _expirationCounter = expirationCounter;
            _poolFactory = poolFactory;
        }

        public override void Subscribe(Deadline obj)
        {
            obj.View.CollisionDetector.Subscribe(this, _expirationLineLayerMask, obj);
        }

        public override void Unsubscribe(Deadline obj)
        {
            obj.View.CollisionDetector.Unsubscribe(this);
        }

        public void TriggerEnter(Collider other, object additionalData = null)
        {
            Deadline deadline = (Deadline)additionalData;
            if(deadline == null) return;
            _poolFactory.Release(deadline);
            _expirationCounter.AddExpiredDeadline();
        }
    }
}