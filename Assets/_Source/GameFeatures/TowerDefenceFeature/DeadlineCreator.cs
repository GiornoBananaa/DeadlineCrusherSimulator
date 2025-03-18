using System;
using System.Linq;
using Core.InstallationSystem.DataLoadingSystem;
using GameFeatures.TowerDefenceFeature.Configs;
using Systems.FactorySystem;
using Systems.ObjectContainerSystem;
using UnityEngine;
using UnityEngine.Pool;
using Object = UnityEngine.Object;

namespace GameFeatures.TowerDefenceFeature
{
    public class DeadlineCreator : PoolFactory<Deadline>
    {
        private readonly ObjectPool<Deadline> _pool;
        private readonly ObjectContainer<Deadline> _deadlinesContainer;
        private readonly DeadlineView _prefab;
        
        public DeadlineCreator(IRepository<ScriptableObject> repository, ObjectContainer<Deadline> deadlinesContainer)
        {
            DeadlinesConfig deadline = repository.GetItem<DeadlinesConfig>().FirstOrDefault();
            if (deadline == null)
                throw new NullReferenceException("No DeadlinesConfig found");
            
            _prefab = deadline.Prefab;
            _pool = new ObjectPool<Deadline>(CreateTask);
            _deadlinesContainer = deadlinesContainer;
        }
        
        public override Deadline Create()
        {
            return _pool.Get();
        }
        
        public override void Release(Deadline deadline)
        {
            _pool.Release(deadline);
            _deadlinesContainer.Remove(deadline);
        }
        
        private Deadline CreateTask()
        {
            Deadline deadline = new Deadline
            {
                View = Object.Instantiate(_prefab)
            };
            _deadlinesContainer.Add(deadline);
            return deadline;
        }
    }
}