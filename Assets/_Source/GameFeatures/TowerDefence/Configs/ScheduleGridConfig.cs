using UnityEngine;

namespace GameFeatures.TowerDefence
{
    [CreateAssetMenu(menuName = "Configs/TowerDefence/ScheduleGridConfig")]
    public class ScheduleGridConfig : ScriptableObject
    {
        [field: SerializeField] public float LinesCount { get; private set; }
        [field: SerializeField] public float RowsCount { get; private set; }
        [field: SerializeField] public float CellXSize { get; private set; }
        [field: SerializeField] public float CellYSize { get; private set; }
    }
}