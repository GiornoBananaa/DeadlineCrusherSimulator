using System;
using System.Linq;
using Core.DataLoading;
using GameFeatures.Clicker;
using UnityEngine;

namespace GameFeatures.WorkProgress
{
    public interface IResettable
    {
        void Reset();
    }
    
    public class WorkCounter : IResettable
    {
        private readonly int _workCountForTask;
        private int _workCounter;
        
        public bool WorkIsDone => _workCounter >= _workCountForTask;
        
        public event Action OnWorkIsDone;
        public event Action<float> OnWorkPercentageChanged;
        public event Action OnNewWork;
        
        public WorkCounter(IRepository<ScriptableObject> repository,ClickCounter clicker)
        {
            WorkProgressConfig config = repository.GetItem<WorkProgressConfig>().FirstOrDefault();
            if (config == null)
                throw new NullReferenceException("No WorkProgressConfig found");
            
            _workCountForTask = config.WorkCountForTask;
            clicker.OnClick += AddWorkProgress;
        }

        public void Reset()
        {
            _workCounter = 0;
            OnNewWork?.Invoke();
            OnWorkPercentageChanged?.Invoke(0);
        }
        
        public bool TryNextWork()
        {
            if (!WorkIsDone) return false;
            _workCounter = 0;
            OnNewWork?.Invoke();
            OnWorkPercentageChanged?.Invoke(0);
            return true;
        }
        
        private void AddWorkProgress()
        {
            _workCounter++;
            OnWorkPercentageChanged?.Invoke(Mathf.InverseLerp(0, _workCountForTask, _workCounter));
            if (_workCounter == _workCountForTask)
            {
                OnWorkIsDone?.Invoke();
            }
        }
    }
}