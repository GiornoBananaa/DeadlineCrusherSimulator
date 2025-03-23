using System.Collections.Generic;
using System.Linq;
using Core.EntitySystem;
using Core.Factory;
using Core.ObjectContainer;
using UnityEngine;

namespace GameFeatures.TowerDefence
{
    public class TaskLifeTimeSystem : ExecuteSystem<Task>
    {
        private readonly IPoolFactory<Task> _poolFactory;

        public TaskLifeTimeSystem(ObjectContainer<Task> objectContainer,
            ServiceUpdater serviceUpdater,
            IPoolFactory<Task> poolFactory) : base(objectContainer, serviceUpdater)
        {
            _poolFactory = poolFactory;
        }
        
        protected override void Execute(IEnumerable<Task> objects)
        {
            foreach (var task in objects.ToArray())
            {
                task.View.SetProgress(task.LifeTimeCounter/task.LifeTime);
                if ((task.LifeTimeCounter += Time.deltaTime) < task.LifeTime) continue;
                
                _poolFactory.Release(task);
            }
        }
    }
}