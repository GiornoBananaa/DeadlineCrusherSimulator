using System.Collections.Generic;
using Systems.GenerationSystem;
using Systems.StateMachineSystem;

namespace Systems.GameStateSystem
{
    public class GameState : IState
    {
        private readonly IEnumerable<IObjectGenerator> _objectGenerators;

        public GameState(IEnumerable<IObjectGenerator> objectGenerators)
        {
            _objectGenerators = objectGenerators;
        }
        
        public void Enter()
        {
            foreach (var generator in _objectGenerators)
            {
                generator.StartGeneration();
            }
        }

        public void Exit()
        {
            foreach (var generator in _objectGenerators)
            {
                generator.StopGeneration();
            }
        }
    }
}