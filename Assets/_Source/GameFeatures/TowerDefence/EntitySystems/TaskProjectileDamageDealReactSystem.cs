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
    public class ProjectileDamageDealReactSystem : ReactiveSystem<TaskProjectile>, ITriggerEnterListener
    {
        private readonly LayerMask _destroyLayerMask;
        private readonly IPoolFactory<TaskProjectile> _projectilePoolFactory;
        
        public ProjectileDamageDealReactSystem(ObjectContainer<TaskProjectile> objectContainer, IRepository<ScriptableObject> dataRepository, IPoolFactory<TaskProjectile> projectilePoolFactory) : base(objectContainer)
        {
            TaskProjectileConfig config = dataRepository.GetItem<TaskProjectileConfig>().FirstOrDefault();
            if(config != null)
                _destroyLayerMask = config.DamageableLayerMask;

            _projectilePoolFactory = projectilePoolFactory;
        }

        public override void Subscribe(TaskProjectile obj)
        {
            obj.View.CollisionDetector.Subscribe(this, _destroyLayerMask, obj);
        }

        public override void Unsubscribe(TaskProjectile obj)
        {
            obj.View.CollisionDetector.Unsubscribe(this);
        }

        public void TriggerEnter(Collider other, object additionalData = null)
        {
            TaskProjectile deadline = (TaskProjectile)additionalData;
            
            if(deadline == null) return;
            
            if(other.attachedRigidbody.TryGetComponent(out GameEntityLinker gameObject) 
               && gameObject.GameEntity is IDamageable damageable)
            {
                damageable.Health -= deadline.Damage;
            }
            
            _projectilePoolFactory.Release(deadline);
        }
    }
}