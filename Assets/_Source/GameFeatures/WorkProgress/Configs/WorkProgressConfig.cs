using UnityEngine;

namespace GameFeatures.WorkProgress
{
    [CreateAssetMenu(menuName = "Configs/TowerDefence/WorkProgressConfig")]
    public class WorkProgressConfig : ScriptableObject
    {
        [field: SerializeField] public int WorkCountForTask { get; private set; }
    }
}