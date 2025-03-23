using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace GameFeatures.WorkProgress
{
    public class ExpirationBarView : MonoBehaviour
    {
        private const string EXPIRED_DEADLINES_TEXT = "Все дедлайны просрочены :(";
        private const string DEFAULT_TEXT = "Время в запасe";
        
        [SerializeField] private Image _progressBar;
        [SerializeField] private TMP_Text _text;
        
        [Inject]
        public void Construct(ExpirationCounter expirationCounter)
        {
            expirationCounter.OnPercentageChanged += OnProgressChanged;
            _progressBar.fillAmount = 1;
            _text.text = DEFAULT_TEXT;
        }
        
        private void OnProgressChanged(float progress)
        {
            _progressBar.fillAmount = 1-progress;
            
            if (Mathf.Approximately(1-progress, 0))
                _text.text = EXPIRED_DEADLINES_TEXT;
            else
                _text.text = DEFAULT_TEXT;
        }
    }
}