using System;
using System.Linq;
using Core.DataLoading;
using GameFeatures.GameState;
using UnityEngine;

namespace GameFeatures.WorkProgress
{
    public class ExpirationCounter : IResettable
    {
        private readonly float _maxExpiredDeadlines;
        private float _expiredDeadlines;
        
        public bool AllDeadlinesExpired { get; private set; }
        
        public event Action<float> OnPercentageChanged;
        public event Action OnAllDeadlinesExpired;

        public ExpirationCounter(IRepository<ScriptableObject> repository)
        {
            WorkExpirationConfig config = repository.GetItem<WorkExpirationConfig>().FirstOrDefault();
            if (config == null)
                throw new NullReferenceException("No WorkExpirationConfig found");
            _maxExpiredDeadlines = config.ExpiredDeadlinesForDefeat; ;
        }

        public void Reset()
        {
            _expiredDeadlines = 0;
            AllDeadlinesExpired = false;
            OnPercentageChanged?.Invoke(0);
        }
        
        public void AddExpiredDeadline()
        {
            if(AllDeadlinesExpired) return;
            
            _expiredDeadlines++;
            OnPercentageChanged?.Invoke(Mathf.InverseLerp(0, _maxExpiredDeadlines, _expiredDeadlines));
            
            if (_expiredDeadlines >= _maxExpiredDeadlines)
            {
                AllDeadlinesExpired = true;
                OnAllDeadlinesExpired?.Invoke();
            }
        }
    }
}