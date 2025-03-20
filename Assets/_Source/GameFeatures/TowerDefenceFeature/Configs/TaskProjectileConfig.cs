using UnityEngine;

namespace GameFeatures.TowerDefenceFeature.Configs
{
    [CreateAssetMenu(menuName = "Configs/TowerDefence/TaskProjectileConfig")]
    public class TaskProjectileConfig : ScriptableObject
    {
        [field: SerializeField] public TaskProjectileView Prefab { get; private set; }
        [field: SerializeField] public LayerMask DestroyLayerMask { get; private set; }
        [field: SerializeField] public Vector3 MoveDirection { get; private set; }
        [field: SerializeField] public float MoveSpeed { get; private set; }
        [field: SerializeField] public float Damage { get; private set; }
    }
}