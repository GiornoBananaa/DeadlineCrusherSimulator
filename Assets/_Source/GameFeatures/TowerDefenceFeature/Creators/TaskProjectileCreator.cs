using System;
using System.Linq;
using Core.DataLoading;
using Core.Factory;
using Core.ObjectContainer;
using GameFeatures.TowerDefenceFeature.Configs;
using UnityEngine;
using UnityEngine.Pool;
using Object = UnityEngine.Object;

namespace GameFeatures.TowerDefenceFeature
{
    public class TaskProjectileCreator : PoolFactory<TaskProjectile>
    {
        private readonly ObjectPool<TaskProjectile> _pool;
        private readonly ObjectContainer<TaskProjectile> _projectileContainer;
        private readonly TaskProjectileView _prefab;
        private readonly Vector3 _moveDirection;
        private readonly float _moveSpeed;
        private readonly float _damage;
        
        public TaskProjectileCreator(IRepository<ScriptableObject> repository, ObjectContainer<TaskProjectile> projectileContainer)
        {
            TaskProjectileConfig config = repository.GetItem<TaskProjectileConfig>().FirstOrDefault();
            if (config == null)
                throw new NullReferenceException("No TaskProjectileConfig found");
            
            _prefab = config.Prefab;
            _damage = config.Damage;
            _pool = new ObjectPool<TaskProjectile>(CreateTask);
            _projectileContainer = projectileContainer;
        }
        
        public override TaskProjectile Create()
        {
            return _pool.Get();
        }
        
        public override void Release(TaskProjectile projectile)
        {
            _pool.Release(projectile);
            _projectileContainer.Remove(projectile);
        }
        
        private TaskProjectile CreateTask()
        {
            TaskProjectile projectile = new TaskProjectile
            {
                View = Object.Instantiate(_prefab),
                MoveDirection = Vector3.forward,
                MoveSpeed = _moveSpeed,
                Damage = _damage,
            };
            
            projectile.View.LinkEntity(projectile);
            
            _projectileContainer.Add(projectile);
            return projectile;
        }
    }
}