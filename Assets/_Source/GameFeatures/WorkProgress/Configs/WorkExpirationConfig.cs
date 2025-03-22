using UnityEngine;

namespace GameFeatures.WorkProgress
{
    [CreateAssetMenu(menuName = "Configs/TowerDefence/WorkExpirationConfig")]
    public class WorkExpirationConfig : ScriptableObject
    {
        [field: SerializeField] public int ExpiredDeadlinesForDefeat { get; private set; }
    }
}