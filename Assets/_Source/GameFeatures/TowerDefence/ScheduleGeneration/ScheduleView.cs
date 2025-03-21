using GameFeatures.PlayerInput;
using UnityEngine;

namespace GameFeatures.TowerDefence.ScheduleGeneration
{
    public class ScheduleView : MonoBehaviour
    {
        [field: SerializeField] public RaycastInputReceiver RaycastInputReceiver { get; private set; }
        [field: SerializeField] public Transform GridPivot { get; private set; }
    }
}