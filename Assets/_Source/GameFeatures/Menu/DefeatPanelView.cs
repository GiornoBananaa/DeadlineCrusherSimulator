using System;
using UnityEngine;
using UnityEngine.UI;

namespace GameFeatures.Menu
{
    public class DefeatPanelView : MonoBehaviour
    {
        [SerializeField] private RectTransform _panel;
        [SerializeField] private Button _replayButton;

        public event Action OnReplayPressed;

        private void Awake()
        {
            _replayButton.onClick.AddListener(PlayPress);
        }

        public void Show()
        {
            _panel.gameObject.SetActive(true);
        }
        
        public void Hide()
        {
            _panel.gameObject.SetActive(false);
        }
        
        private void PlayPress()
        {
            OnReplayPressed?.Invoke();
        }
    }
}