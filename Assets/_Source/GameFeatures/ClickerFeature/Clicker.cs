﻿using System;

namespace GameFeatures.ClickerFeature
{
    public class Clicker
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