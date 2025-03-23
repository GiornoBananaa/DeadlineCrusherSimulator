using UnityEngine;

namespace GameFeatures.WorkProgress
{
    [CreateAssetMenu(menuName = "Configs/WorkProgressConfig")]
    public class WorkProgressConfig : ScriptableObject
    {
        [field: SerializeField] public int WorkCountForTask { get; private set; }
    }
}