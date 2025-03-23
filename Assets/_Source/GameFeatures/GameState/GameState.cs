using System.Collections.Generic;
using Core.Generation;
using Core.StateMachine;
using GameFeatures.PlayerInput;
using GameFeatures.TowerDefence;
using GameFeatures.WorkProgress;

namespace GameFeatures.GameState
{
    public class GameState : IState
    {
        private readonly IEnumerable<IObjectGenerator> _objectGenerators;
        private readonly IEnumerable<IResettable> _resettable;
        private readonly EntityResetter _entityResetter;
        private readonly ExpirationCounter _expirationCounter;
        private readonly InputListener _inputListener;
        private StateMachine _owner;
        
        public GameState(IEnumerable<IObjectGenerator> objectGenerators, IEnumerable<IResettable> resettable, 
            EntityResetter entityResetter, ExpirationCounter expirationCounter, InputListener inputListener)
        {
            _objectGenerators = objectGenerators;
            _resettable = resettable;
            _entityResetter = entityResetter;
            _expirationCounter = expirationCounter;
            _inputListener = inputListener;
        }

        public void SetOwner(StateMachine owner)
        {
            _owner = owner;
        }

        public void Enter()
        {
            foreach (var generator in _objectGenerators)
            {
                generator.StartGeneration();
            }

            foreach (var resettable in _resettable)
            {
                resettable.Reset();
            }
            
            _inputListener.EnableInput();
            
            _expirationCounter.OnAllDeadlinesExpired += OnAllDeadlinesExpired;
        }
        
        public void Exit()
        {
            _expirationCounter.OnAllDeadlinesExpired -= OnAllDeadlinesExpired;
            
            _entityResetter.Reset();
            _inputListener.DisableInput();
            
            foreach (var generator in _objectGenerators)
            {
                generator.StopGeneration();
            }
        }

        private void OnAllDeadlinesExpired()
        {
            _owner.ChangeState<DefeatState>();
        }
    }
}