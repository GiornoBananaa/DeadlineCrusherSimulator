using UnityEngine;

namespace GameFeatures.TowerDefenceFeature.Configs
{
    [CreateAssetMenu(menuName = "Configs/TowerDefence/GenerationConfig")]
    public class GenerationConfig : ScriptableObject
    {
        [field: SerializeField] public float DeadlinesGenerationCooldown { get; private set; }
    }
}