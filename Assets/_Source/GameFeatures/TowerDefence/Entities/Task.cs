using System;
using Core.EntitySystem;
using UnityEngine;

namespace GameFeatures.TowerDefence
{
    public class Task : IGameEntity, IDamageable
    {
        private float _health;
        
        public TaskView View { get; set; }
        public float ShootingCooldown { get; set; }
        public float ShootingCounter { get; set; }
        
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
        
        public event Action<Task,float> OnHealthChanged;
    }
}