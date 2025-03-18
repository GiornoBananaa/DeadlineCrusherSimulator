using UnityEngine;

namespace GameFeatures.TowerDefenceFeature
{
    public class TaskView : MonoBehaviour
    {
        [SerializeField] private MeshRenderer _meshRenderer;
        
        [field: SerializeField] public Transform ShootPoint { get; private set; }
        
        public void SetColor(Color color)
        {
            _meshRenderer.material.color = color;
        }
    }
}