using UnityEngine;

namespace GameFeatures.TowerDefence.Configs
{
    [CreateAssetMenu(menuName = "Configs/TowerDefence/GenerationConfig")]
    public class GenerationConfig : ScriptableObject
    {
        [field: SerializeField] public float DeadlinesGenerationCooldown { get; private set; }
    }
}