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
    public class TaskProjectileCreator : IPoolFactory<TaskProjectile>
    {
        private readonly ObjectPool<TaskProjectile> _pool;
        private readonly ObjectContainer<TaskProjectile> _projectileContainer;
        private readonly TaskProjectileView _prefab;
        private readonly float _moveSpeed;
        private readonly float _damage;
        private readonly float _lifeTime;
        
        public TaskProjectileCreator(IRepository<ScriptableObject> repository, ObjectContainer<TaskProjectile> projectileContainer)
        {
            TaskProjectileConfig config = repository.GetItem<TaskProjectileConfig>().FirstOrDefault();
            if (config == null)
                throw new NullReferenceException("No TaskProjectileConfig found");
            
            _prefab = config.Prefab;
            _moveSpeed = config.MoveSpeed;
            _damage = config.Damage;
            _lifeTime = config.LifeTime;
            _pool = new ObjectPool<TaskProjectile>(CreateTask);
            _projectileContainer = projectileContainer;
        }
        
        public TaskProjectile Create()
        {
            TaskProjectile projectile = _pool.Get();
            projectile.View.gameObject.SetActive(true);
            ResetValues(projectile);
            _projectileContainer.Add(projectile);
            return projectile;
        }
        
        public void Release(TaskProjectile projectile)
        {
            projectile.View.gameObject.SetActive(false);
            _pool.Release(projectile);
            _projectileContainer.Remove(projectile);
        }
        
        private TaskProjectile CreateTask()
        {
            TaskProjectile projectile = new TaskProjectile
            {
                View = Object.Instantiate(_prefab)
            };
            ResetValues(projectile);
            projectile.View.LinkEntity(projectile);
            return projectile;
        }
        
        private void ResetValues(TaskProjectile projectile)
        {
            projectile.MoveSpeed = _moveSpeed;
            projectile.LifeTime = _lifeTime;
            projectile.LifeTimeCounter = 0;
            projectile.Damage = _damage;
        }
    }
}