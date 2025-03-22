using System;
using System.Linq;
using Core.DataLoading;
using GameFeatures.GameState;
using UnityEngine;

namespace GameFeatures.WorkProgress
{
    public class ExpirationCounter
    {
        private readonly GameStateMachine _gameStateMachine;
        private float _expiredDeadlines;
        private float _maxExpiredDeadlines;
        
        public event Action<float> OnPercentageChanged;
        public event Action OnAllDeadlinesExpired;

        public ExpirationCounter(IRepository<ScriptableObject> repository, GameStateMachine gameStateMachine)
        {
            WorkExpirationConfig config = repository.GetItem<WorkExpirationConfig>().FirstOrDefault();
            if (config == null)
                throw new NullReferenceException("No WorkExpirationConfig found");

            _gameStateMachine = gameStateMachine;
        }
        
        public void AddExpiredDeadline()
        {
            _expiredDeadlines++;
            OnPercentageChanged?.Invoke(Mathf.InverseLerp(0, _maxExpiredDeadlines, _expiredDeadlines));
            
            if (_expiredDeadlines > _maxExpiredDeadlines)
            {
                OnAllDeadlinesExpired?.Invoke();
                _gameStateMachine.SetState(GameStates.Defeat);
            }
        }
    }
}