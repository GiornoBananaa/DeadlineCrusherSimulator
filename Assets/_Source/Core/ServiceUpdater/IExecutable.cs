using System.Collections.Generic;
using Zenject;

namespace Core.ServiceUpdater
{
    public interface IExecutable
    {
        void Execute();
    }

    public class ServiceUpdater: ITickable
    {
        private readonly List<IExecutable> _executables = new();
        private readonly Queue<IExecutable> _subscribeQueue = new();
        private readonly Queue<IExecutable> _unsubscribeQueue = new();

        public void Subscribe(IExecutable updatable)
        {
            _subscribeQueue.Enqueue(updatable);
        }

        public void Unsubscribe(IExecutable updatable)
        {
            _unsubscribeQueue.Enqueue(updatable);
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
    }
}