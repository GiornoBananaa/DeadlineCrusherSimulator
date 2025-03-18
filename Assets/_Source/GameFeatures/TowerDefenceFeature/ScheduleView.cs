using UnityEngine;

namespace GameFeatures.TowerDefenceFeature
{
    public class ScheduleView : MonoBehaviour
    {
        [field: SerializeField] public float LineLength { get; private set; }
        [field: SerializeField] public Transform[] LineStartPoints { get; private set; }
    }
}