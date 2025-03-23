using System;
using UnityEngine;
using UnityEngine.UI;

namespace GameFeatures.Menu
{
    public class MenuPanelView : MonoBehaviour
    {
        [SerializeField] private RectTransform _panel;
        [SerializeField] private Button _playButton;

        public event Action OnPlayPressed;

        private void Awake()
        {
            _playButton.onClick.AddListener(PlayPress);
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
            OnPlayPressed?.Invoke();
        }
    }
}