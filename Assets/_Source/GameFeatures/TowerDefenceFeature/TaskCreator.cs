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
    public class TaskCreator : PoolFactory<Task>
    {
        private readonly ObjectPool<Task> _pool;
        private readonly ObjectContainer<Task> _tasksContainer;
        private readonly TaskView _prefab;
        
        public TaskCreator(IRepository<ScriptableObject> repository, ObjectContainer<Task> tasksContainer)
        {
            TasksConfig tasksConfig = repository.GetItem<TasksConfig>().FirstOrDefault();
            if (tasksConfig == null)
                throw new NullReferenceException("No TasksConfig found");
            
            _prefab = tasksConfig.Prefab;
            _pool = new ObjectPool<Task>(CreateTask);
            _tasksContainer = tasksContainer;
        }
        
        public override Task Create()
        {
            return _pool.Get();
        }
        
        public override void Release(Task task)
        {
            _pool.Release(task);
            _tasksContainer.Remove(task);
        }
        
        private Task CreateTask()
        {
            Task task = new Task
            {
                View = Object.Instantiate(_prefab)
            };
            _tasksContainer.Add(task);
            return task;
        }
    }
}