using Core.EntitySystem;
using Core.PhysicsDetection;
using UnityEngine;
using UnityEngine.UI;

namespace GameFeatures.TowerDefence
{
    public class TaskView : GameEntityLinker
    {
        [SerializeField] private MeshRenderer _meshRenderer;
        [SerializeField] private Image _fillImage;
        
        [field: SerializeField] public CollisionDetector CollisionDetector { get; private set; }
        [field: SerializeField] public Transform ShootPoint { get; private set; }
        
        public void SetColor(Color color)
        {
            _meshRenderer.material.color = color;
        }
        
        public void SetProgress(float progress)
        {
            _fillImage.fillAmount = progress;
        }
    }
}