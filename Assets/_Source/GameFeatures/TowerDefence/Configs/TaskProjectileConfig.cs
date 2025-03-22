using UnityEngine;

namespace GameFeatures.TowerDefence.Configs
{
    [CreateAssetMenu(menuName = "Configs/TowerDefence/TaskProjectileConfig")]
    public class TaskProjectileConfig : ScriptableObject
    {
        [field: SerializeField] public TaskProjectileView Prefab { get; private set; }
        [field: SerializeField] public LayerMask DamageableLayerMask { get; private set; }
        [field: SerializeField] public float LifeTime { get; private set; }
        [field: SerializeField] public float MoveSpeed { get; private set; }
        [field: SerializeField] public float Damage { get; private set; }
    }
}