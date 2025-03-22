using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace GameFeatures.WorkProgress
{
    public class WorkProgressView : MonoBehaviour
    {
        [SerializeField] private Image _progressBar;
        
        [Inject]
        public void Construct(WorkCounter workCounter)
        {
            workCounter.OnWorkPercentageChanged += OnProgressChanged;
        }

        private void OnProgressChanged(float progress)
        {
            _progressBar.fillAmount = progress;
        }
    }
}