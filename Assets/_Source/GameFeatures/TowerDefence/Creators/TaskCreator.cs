using System;
using System.Collections.Generic;
using System.Linq;
using Core.DataLoading;
using Core.EntitySystem;
using Core.Factory;
using Core.ObjectContainer;
using GameFeatures.TowerDefence.Configs;
using UnityEngine;
using UnityEngine.Pool;
using Object = UnityEngine.Object;
using Random = UnityEngine.Random;

namespace GameFeatures.TowerDefence
{
    public class TaskCreator : IPoolFactory<Task>
    {
        private readonly ObjectPool<Task> _pool;
        private readonly ObjectContainer<Task> _tasksContainer;
        private readonly TaskView _prefab;
        private readonly Color[] _colors;
        private readonly float _shootingCooldown;
        private readonly float _health;
        private readonly float _lifeTime;
        
        public TaskCreator(IRepository<ScriptableObject> repository, ObjectContainer<Task> tasksContainer)
        {
            TasksConfig tasksConfig = repository.GetItem<TasksConfig>().FirstOrDefault();
            if (tasksConfig == null)
                throw new NullReferenceException("No TasksConfig found");
            
            _prefab = tasksConfig.Prefab;
            _shootingCooldown = tasksConfig.ShootingCooldown;
            _health = tasksConfig.Health;
            _colors = tasksConfig.Colors;
            _lifeTime = tasksConfig.LifeTime;
            _pool = new ObjectPool<Task>(CreateTask);
            _tasksContainer = tasksContainer;
        }
        
        public Task Create()
        {
            Task task = _pool.Get();
            task.View.gameObject.SetActive(true);
            ResetValues(task);
            _tasksContainer.Add(task);
            return task;
        }
        
        public void Release(Task projectile)
        {
            projectile.View.gameObject.SetActive(false);
            _pool.Release(projectile);
            _tasksContainer.Remove(projectile);
        }
        
        private Task CreateTask()
        {
            Task task = new Task
            {
                View = Object.Instantiate(_prefab),
            };
            ResetValues(task);
            task.View.LinkEntity(task);
            task.View.SetColor(_colors[Random.Range(0, _colors.Length)]);
            return task;
        }
        
        private void ResetValues(Task task)
        {
            task.ShootingCooldown = _shootingCooldown;
            task.ShootingCounter = _shootingCooldown;
            task.Health = _health;
            task.LifeTime = _lifeTime;
            task.LifeTimeCounter = 0;
        }
    }
}