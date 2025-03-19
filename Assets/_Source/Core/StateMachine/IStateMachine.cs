using System;
using System.Collections.Generic;

namespace Core.StateMachine
{
    public class StateMachine
    {
        private readonly Dictionary<Type,IState> _states = new Dictionary<Type,IState>();
        private readonly IState _currentState;
        
        public StateMachine(params IState[] states)
        {
            foreach (IState state in states)
            {
                _states.Add(state.GetType(), state);
            }

            _currentState = null;
        }
        
        public void ChangeState<T>() where T : IState
        {
            Type stateType = typeof(T);
            IState newState = _states[stateType];
            if(newState == _currentState) return;
            _currentState?.Exit();
            newState.Enter();
        }
    }
}