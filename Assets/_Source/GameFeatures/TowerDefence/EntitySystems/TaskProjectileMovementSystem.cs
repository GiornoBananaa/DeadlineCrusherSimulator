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

        protected override void Execute(IEnumerable<TaskProjectile> objects)
        {
            foreach (var projectile in objects)
            {
                projectile.View.transform.position += projectile.MoveDirection.normalized * (projectile.MoveSpeed * Time.deltaTime);
            }
        }
    }
}