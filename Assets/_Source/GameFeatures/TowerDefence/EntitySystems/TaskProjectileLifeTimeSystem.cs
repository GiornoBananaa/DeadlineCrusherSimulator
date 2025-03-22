using System.Collections.Generic;
using System.Linq;
using Core.EntitySystem;
using Core.Factory;
using Core.ObjectContainer;
using UnityEngine;

namespace GameFeatures.TowerDefence
{
    public class TaskProjectileLifeTimeSystem : ExecuteSystem<TaskProjectile>
    {
        private readonly IPoolFactory<TaskProjectile> _poolFactory;

        public TaskProjectileLifeTimeSystem(ObjectContainer<TaskProjectile> objectContainer,
            ServiceUpdater serviceUpdater,
            IPoolFactory<TaskProjectile> poolFactory) : base(objectContainer, serviceUpdater)
        {
            _poolFactory = poolFactory;
        }
        
        protected override void Execute(IEnumerable<TaskProjectile> objects)
        {
            foreach (var projectile in objects.ToArray())
            {
                if ((projectile.LifeTimeCounter += Time.deltaTime) < projectile.LifeTime) continue;
                
                _poolFactory.Release(projectile);
            }
        }
    }
}