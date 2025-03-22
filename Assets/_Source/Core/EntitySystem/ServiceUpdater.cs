using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Core.EntitySystem
{
    public enum UpdateMode
    {
        Default = 0,
        Fixed = 1,
        Late = 2,
    }
    
    public class ServiceUpdater: ITickable, IFixedTickable, ILateTickable
    {
        private readonly List<IExecutable> _executables = new();
        private readonly Queue<IExecutable> _subscribeQueue = new();
        private readonly Queue<IExecutable> _unsubscribeQueue = new();
        
        private readonly List<IExecutable> _executablesFixed = new();
        private readonly Queue<IExecutable> _subscribeQueueFixed = new();
        private readonly Queue<IExecutable> _unsubscribeQueueFixed = new();
        
        private readonly List<IExecutable> _executablesLate = new();
        private readonly Queue<IExecutable> _subscribeQueueLate = new();
        private readonly Queue<IExecutable> _unsubscribeQueueLate = new();

        public void Subscribe(IExecutable updatable, UpdateMode mode = UpdateMode.Default)
        {
            switch (mode)
            {
                case UpdateMode.Default:
                    _subscribeQueue.Enqueue(updatable);
                    break;
                case UpdateMode.Fixed:
                    _subscribeQueueFixed.Enqueue(updatable);
                    break;
                case UpdateMode.Late:
                    _subscribeQueueLate.Enqueue(updatable);
                    break;
            }
        }
        
        public void Unsubscribe(IExecutable updatable, UpdateMode mode = UpdateMode.Default)
        {
            switch (mode)
            {
                case UpdateMode.Default:
                    _unsubscribeQueue.Enqueue(updatable);
                    break;
                case UpdateMode.Fixed:
                    _unsubscribeQueueFixed.Enqueue(updatable);
                    break;
                case UpdateMode.Late:
                    _unsubscribeQueueLate.Enqueue(updatable);
                    break;
            }
        }

        public void Tick()
        {
            foreach (var executable in _executables)
            {
                executable.Execute();
            }
            while (_unsubscribeQueue.Count > 0)
            {
                _executables.Remove(_unsubscribeQueue.Dequeue());
            }
            while (_subscribeQueue.Count > 0)
            {
                _executables.Add(_subscribeQueue.Dequeue());
            }
        }

        public void FixedTick()
        {
            foreach (var executable in _executablesFixed)
            {
                executable.Execute();
            }
            while (_unsubscribeQueueFixed.Count > 0)
            {
                _executablesFixed.Remove(_unsubscribeQueueFixed.Dequeue());
            }
            while (_subscribeQueueFixed.Count > 0)
            {
                _executablesFixed.Add(_subscribeQueueFixed.Dequeue());
            }
        }

        public void LateTick()
        {
            foreach (var executable in _executablesLate)
            {
                executable.Execute();
            }
            while (_unsubscribeQueueLate.Count > 0)
            {
                _executablesLate.Remove(_unsubscribeQueueLate.Dequeue());
            }
            while (_subscribeQueueLate.Count > 0)
            {
                _executablesLate.Add(_subscribeQueueLate.Dequeue());
            }
        }
    }
}