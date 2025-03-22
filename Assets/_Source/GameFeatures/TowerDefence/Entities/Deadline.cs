using System;
using Core.EntitySystem;
using UnityEngine;

namespace GameFeatures.TowerDefence
{
    public class Deadline : IGameEntity, IDamageable
    {
        private float _health;
        
        public DeadlineView View { get; set; }
        public Vector3 MoveDirection { get; set; }
        public float MoveSpeed { get; set; }
        public float Damage { get; set; }
        
        public float Health
        {
            get => _health;
            set
            {
                bool changed = !Mathf.Approximately(_health, value);
                _health = value;
                if(changed)
                    OnHealthChanged?.Invoke(this, _health);
            }
        }

        public event Action<Deadline, float> OnHealthChanged;
    }
}