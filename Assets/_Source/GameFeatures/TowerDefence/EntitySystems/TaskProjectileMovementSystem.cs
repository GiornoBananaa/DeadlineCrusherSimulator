using System.Collections.Generic;
using Core.EntitySystem;
using Core.ObjectContainer;
using UnityEngine;

namespace GameFeatures.TowerDefence
{
    public class TaskProjectileMovementSystem : ExecuteSystem<TaskProjectile>
    {
        public TaskProjectileMovementSystem(ObjectContainer<TaskProjectile> objectContainer, ServiceUpdater serviceUpdater) : base(objectContainer, serviceUpdater)
        { }

        protected override UpdateMode ExecuteMode => UpdateMode.Fixed;
        
        protected override void Execute(IEnumerable<TaskProjectile> objects)
        {
            foreach (var projectile in objects)
            {
                Vector3 position = projectile.View.transform.position + projectile.View.transform.forward * (projectile.MoveSpeed * Time.fixedDeltaTime);
                projectile.View.Rigidbody.MovePosition(position);
            }
        }
    }
}