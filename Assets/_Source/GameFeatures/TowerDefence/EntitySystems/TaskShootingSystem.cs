using System.Collections.Generic;
using Core.EntitySystem;
using Core.Factory;
using Core.ObjectContainer;
using UnityEngine;

namespace GameFeatures.TowerDefence
{
    public class TaskShootingSystem : ExecuteSystem<Task>
    {
        private readonly IPoolFactory<TaskProjectile> _projectileFactory;
        
        public TaskShootingSystem(ObjectContainer<Task> objectContainer, ServiceUpdater serviceUpdater,
            IPoolFactory<TaskProjectile> projectileFactory) : base(objectContainer, serviceUpdater)
        {
            _projectileFactory = projectileFactory;
        }

        protected override void Execute(IEnumerable<Task> objects)
        {
            foreach (Task task in objects)
            {
                if ((task.ShootingCounter -= Time.deltaTime) > 0) continue;
                
                task.ShootingCounter = task.ShootingCooldown;
                TaskProjectile projectile = _projectileFactory.Create();
                projectile.View.transform.position = task.View.ShootPoint.position;
                projectile.View.transform.rotation = task.View.ShootPoint.rotation;
            }
        }
    }
}