using UnityEngine;

namespace GameFeatures.TowerDefenceFeature
{
    public class ScheduleView : MonoBehaviour
    {
        [field: SerializeField] public Transform[] LineStartPoints { get; private set; }
        [field: SerializeField] public Transform[] LineEndPoints { get; private set; }
    }
}