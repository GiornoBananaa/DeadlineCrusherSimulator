using System;
using Core.EntitySystem;
using UnityEngine;

namespace GameFeatures.TowerDefenceFeature
{
    public class Deadline : IGameEntity, IDamageable
    {
        private float _health;
        
        public DeadlineView View { get; set; }
        public Vector3 MoveDirection { get; set; }
        public float MoveSpeed { get; set; }
        
        public float Health
        {
            get => _health;
            set
            {
                _health = value;
                OnHealthChanged?.Invoke(_health);
            }
        }

        public event Action<float> OnHealthChanged;
    }
}