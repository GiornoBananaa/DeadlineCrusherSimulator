using System.Collections.Generic;
using Core.ObjectContainer;
using Core.ServiceUpdater;
using UnityEngine;

namespace GameFeatures.TowerDefenceFeature
{
    public class DeadlineMovementSystem : ExecuteSystem<Deadline>
    {
        public DeadlineMovementSystem(ObjectContainer<Deadline> objectContainer, ServiceUpdater serviceUpdater) : base(objectContainer, serviceUpdater)
        { }

        protected override void Execute(IEnumerable<Deadline> objects)
        {
            foreach (var deadline in objects)
            {
                deadline.View.transform.position += deadline.MoveDirection.normalized * deadline.MoveSpeed * Time.deltaTime;
            }
        }
    }
}
