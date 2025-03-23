using System;
using GameFeatures.WorkProgress;

namespace GameFeatures.Clicker
{
    public class ClickCounter : IResettable
    {
        private uint _clicks;

        public event Action OnClick;
        
        public uint Clicks => _clicks;
        
        public void Click()
        {
            _clicks++;
            OnClick?.Invoke();
        }
        
        public void Reset()
        {
            _clicks = 0;
        }
    }
}