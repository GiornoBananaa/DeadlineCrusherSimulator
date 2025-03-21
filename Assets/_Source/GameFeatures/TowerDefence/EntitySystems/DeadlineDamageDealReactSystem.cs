using System.Linq;
using Core.DataLoading;
using Core.EntitySystem;
using Core.Factory;
using Core.ObjectContainer;
using Core.PhysicsDetection;
using GameFeatures.TowerDefence.Configs;
using UnityEngine;

namespace GameFeatures.TowerDefence
{
    public class DeadlineDamageDealReactSystem : ReactiveSystem<Deadline>, ICollisionEnterListener
    {
        private readonly LayerMask _damageableLayerMask;
        
        public DeadlineDamageDealReactSystem(ObjectContainer<Deadline> objectContainer, DataRepository<ScriptableObject> dataRepository, PoolFactory<Deadline> projectilePoolFactory) : base(objectContainer)
        {
            DeadlinesConfig config = dataRepository.GetItem<DeadlinesConfig>().FirstOrDefault();
            if(config != null)
                _damageableLayerMask = config.DamageableLayerMask;
        }

        public override void Subscribe(Deadline obj)
        {
            obj.View.CollisionDetector.Subscribe(this, _damageableLayerMask, obj);
        }
        
        public void CollisionEnter(Collision other, object additionalData = null)
        {
            Deadline deadline = (Deadline)additionalData;
            if(deadline == null) return;
            
            if(other.collider.TryGetComponent(out GameEntityLinker gameObject) 
               && gameObject.GameEntity is IDamageable damageable)
            {
                damageable.Health = 0;
            }
        }
    }
}