﻿using System;
using System.Linq;
using GameFeatures.TowerDefenceFeature.Configs;
using Core.DataLoading;
using Core.Factory;
using Core.ObjectContainer;
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
        private readonly Vector3 _moveDirection;
        private readonly float _moveSpeed;
        
        public DeadlineCreator(IRepository<ScriptableObject> repository, ObjectContainer<Deadline> deadlinesContainer)
        {
            DeadlinesConfig deadline = repository.GetItem<DeadlinesConfig>().FirstOrDefault();
            if (deadline == null)
                throw new NullReferenceException("No DeadlinesConfig found");
            
            _prefab = deadline.Prefab;
            _moveDirection = deadline.MoveDirection;
            _moveSpeed = deadline.MoveSpeed;
            
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
                View = Object.Instantiate(_prefab),
                MoveDirection = _moveDirection,
                MoveSpeed = _moveSpeed
            };
            _deadlinesContainer.Add(deadline);
            return deadline;
        }
    }
}