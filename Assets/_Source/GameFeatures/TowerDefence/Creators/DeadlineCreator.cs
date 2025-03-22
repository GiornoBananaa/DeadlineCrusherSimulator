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
    public class DeadlineCreator : IPoolFactory<Deadline>
    {
        private readonly ObjectPool<Deadline> _pool;
        private readonly ObjectContainer<Deadline> _deadlinesContainer;
        private readonly DeadlineView _prefab;
        private readonly Vector3 _moveDirection;
        private readonly float _moveSpeed;
        private readonly float _health;
        private readonly float _damage;
        
        public DeadlineCreator(IRepository<ScriptableObject> repository, ObjectContainer<Deadline> deadlinesContainer)
        {
            DeadlinesConfig deadline = repository.GetItem<DeadlinesConfig>().FirstOrDefault();
            if (deadline == null)
                throw new NullReferenceException("No DeadlinesConfig found");
            
            _prefab = deadline.Prefab;
            _moveDirection = deadline.MoveDirection;
            _moveSpeed = deadline.MoveSpeed;
            _health = deadline.Health;
            _damage = deadline.Damage;
            
            _pool = new ObjectPool<Deadline>(CreateTask);
            _deadlinesContainer = deadlinesContainer;
        }
        
        public Deadline Create()
        {
            Deadline deadline = _pool.Get();
            ResetValues(deadline);
            deadline.View.gameObject.SetActive(true);
            _deadlinesContainer.Add(deadline);
            return deadline;
        }
        
        public void Release(Deadline projectile)
        {
            projectile.View.gameObject.SetActive(false);
            _deadlinesContainer.Remove(projectile);
            _pool.Release(projectile);
        }
        
        private Deadline CreateTask()
        {
            Deadline deadline = new Deadline
            {
                View = Object.Instantiate(_prefab),
            };
            ResetValues(deadline);
            deadline.View.LinkEntity(deadline);
            return deadline;
        }
        
        private void ResetValues(Deadline deadline)
        {
            deadline.MoveDirection = _moveDirection;
            deadline.MoveSpeed = _moveSpeed;
            deadline.Damage = _damage;
            deadline.Health = _health;
        }
    }
}