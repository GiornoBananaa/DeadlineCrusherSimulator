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
    public class ProjectileDamageDealReactSystem : ReactiveSystem<TaskProjectile>, ICollisionEnterListener
    {
        private readonly LayerMask _destroyLayerMask;
        private readonly PoolFactory<TaskProjectile> _projectilePoolFactory;
        
        public ProjectileDamageDealReactSystem(ObjectContainer<TaskProjectile> objectContainer, DataRepository<ScriptableObject> dataRepository, PoolFactory<TaskProjectile> projectilePoolFactory) : base(objectContainer)
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
        
        public void CollisionEnter(Collision other, object additionalData = null)
        {
            TaskProjectile deadline = (TaskProjectile)additionalData;
            if(deadline == null) return;
            
            if(other.collider.TryGetComponent(out GameEntityLinker gameObject) 
               && gameObject.GameEntity is IDamageable damageable)
            {
                damageable.Health -= deadline.Damage;
            }
            
            _projectilePoolFactory.Release(deadline);
        }
    }
}