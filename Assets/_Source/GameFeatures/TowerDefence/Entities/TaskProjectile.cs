using Core.EntitySystem;
using UnityEngine;

namespace GameFeatures.TowerDefence
{
    public class TaskProjectile : IGameEntity
    {
        public TaskProjectileView View { get; set; }
        public float MoveSpeed { get; set; }
        public float Damage { get; set; }
        public float LifeTime { get; set; }
        public float LifeTimeCounter { get; set; }
    }
}