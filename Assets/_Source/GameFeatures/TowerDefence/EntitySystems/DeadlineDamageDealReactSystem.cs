using System.Linq;
using Core.DataLoading;
using Core.EntitySystem;
using Core.ObjectContainer;
using Core.PhysicsDetection;
using GameFeatures.TowerDefence.Configs;
using UnityEngine;

namespace GameFeatures.TowerDefence
{
    public class DeadlineDamageDealReactSystem : ReactiveSystem<Deadline>, ITriggerEnterListener
    {
        private readonly LayerMask _damageableLayerMask;
        
        public DeadlineDamageDealReactSystem(ObjectContainer<Deadline> objectContainer, IRepository<ScriptableObject> dataRepository) : base(objectContainer)
        {
            DeadlinesConfig config = dataRepository.GetItem<DeadlinesConfig>().FirstOrDefault();
            if(config != null)
                _damageableLayerMask = config.DamageableLayerMask;
        }

        public override void Subscribe(Deadline obj)
        {
            obj.View.CollisionDetector.Subscribe(this, _damageableLayerMask, obj);
        }

        public override void Unsubscribe(Deadline obj)
        {
            obj.View.CollisionDetector.Unsubscribe(this);
        }

        public void TriggerEnter(Collider other, object additionalData = null)
        {
            Deadline deadline = (Deadline)additionalData;
            if(deadline == null) return;
            
            if(other.attachedRigidbody.TryGetComponent(out GameEntityLinker gameObject) 
               && gameObject.GameEntity is IDamageable damageable)
            {
                damageable.Health -= deadline.Damage;
            }
        }
    }
}