using Core.EntitySystem;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace GameFeatures.WorkProgress
{
    public class ExpirationBarView : MonoBehaviour
    {
        [SerializeField] private Image _progressBar;
        
        [Inject]
        public void Construct(ExpirationCounter expirationCounter)
        {
            expirationCounter.OnPercentageChanged += OnProgressChanged;
        }

        private void OnProgressChanged(float progress)
        {
            _progressBar.fillAmount = progress;
        }
    }
}