using Core.EntitySystem;
using UnityEngine;

namespace GameFeatures.TowerDefenceFeature
{
    public class TaskProjectile : IGameEntity
    {
        public TaskProjectileView View { get; set; }
        public Vector3 MoveDirection { get; set; }
        public float MoveSpeed { get; set; }
        public float Damage { get; set; }
    }
}