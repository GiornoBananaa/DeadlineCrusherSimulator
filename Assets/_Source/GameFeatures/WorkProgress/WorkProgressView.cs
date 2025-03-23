using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace GameFeatures.WorkProgress
{
    public class WorkProgressView : MonoBehaviour
    {
        [SerializeField] private Image _progressBar;
        [SerializeField] private TMP_Text _text;
        
        [Inject]
        public void Construct(WorkCounter workCounter)
        {
            workCounter.OnWorkPercentageChanged += OnProgressChanged;
        }

        private void OnProgressChanged(float progress)
        {
            _progressBar.fillAmount = progress;
            _text.gameObject.SetActive(Mathf.Approximately(progress, 1));
        }
    }
}