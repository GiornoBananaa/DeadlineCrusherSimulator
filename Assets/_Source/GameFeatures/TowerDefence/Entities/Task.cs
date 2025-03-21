using System;
using Core.EntitySystem;

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
                _health = value;
                OnHealthChanged?.Invoke(_health);
            }
        }
        
        public event Action<float> OnHealthChanged;
    }
}