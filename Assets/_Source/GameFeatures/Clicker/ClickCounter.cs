using System;

namespace GameFeatures.Clicker
{
    public class ClickCounter
    {
        private uint _clicks;

        public event Action OnClick;
        
        public uint Clicks => _clicks;
        
        public void Click()
        {
            _clicks++;
            OnClick?.Invoke();
        }
    }
}