using System;
using System.Linq;
using Core.DataLoading;
using Core.Factory;
using Core.ObjectContainer;
using GameFeatures.TowerDefence.Configs;
using UnityEngine;
using UnityEngine.Pool;
using Object = UnityEngine.Object;

namespace GameFeatures.TowerDefence
{
    public class TaskCreator : PoolFactory<Task>
    {
        private readonly ObjectPool<Task> _pool;
        private readonly ObjectContainer<Task> _tasksContainer;
        private readonly TaskView _prefab;
        private readonly float _shootingCooldown;
        private readonly float _health;
        public TaskCreator(IRepository<ScriptableObject> repository, ObjectContainer<Task> tasksContainer)
        {
            TasksConfig tasksConfig = repository.GetItem<TasksConfig>().FirstOrDefault();
            if (tasksConfig == null)
                throw new NullReferenceException("No TasksConfig found");
            
            _prefab = tasksConfig.Prefab;
            _shootingCooldown = tasksConfig.ShootingCooldown;
            _health = tasksConfig.Health;
            _pool = new ObjectPool<Task>(CreateTask);
            _tasksContainer = tasksContainer;
        }
        
        public override Task Create()
        {
            return _pool.Get();
        }
        
        public override void Release(Task projectile)
        {
            _pool.Release(projectile);
            _tasksContainer.Remove(projectile);
        }
        
        private Task CreateTask()
        {
            Task task = new Task
            {
                View = Object.Instantiate(_prefab),
                ShootingCooldown = _shootingCooldown,
                ShootingCounter = _shootingCooldown,
                Health = _health,
            };
            
            task.View.LinkEntity(task);
            
            _tasksContainer.Add(task);
            return task;
        }
    }
}