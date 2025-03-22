using System.Collections.Generic;
using Core.EntitySystem;
using Core.ObjectContainer;
using UnityEngine;

namespace GameFeatures.TowerDefence
{
    public class DeadlineMovementSystem : ExecuteSystem<Deadline>
    {
        public DeadlineMovementSystem(ObjectContainer<Deadline> objectContainer, ServiceUpdater serviceUpdater) : base(objectContainer, serviceUpdater)
        { }
        
        protected override UpdateMode ExecuteMode => UpdateMode.Fixed;
        
        protected override void Execute(IEnumerable<Deadline> objects)
        {
            foreach (var deadline in objects)
            {
                Vector3 position = deadline.View.transform.position + deadline.MoveDirection * (deadline.MoveSpeed * Time.fixedDeltaTime);
                deadline.View.Rigidbody.MovePosition(position);
            }
        }
    }
}
