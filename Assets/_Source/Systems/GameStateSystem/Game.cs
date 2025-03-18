using System.Collections.Generic;
using Systems.GenerationSystem;
using Systems.StateMachineSystem;

namespace Systems.GameStateSystem
{
    public enum GameStates
    {
        Game = 0,
        Defeat = 1
    }
    
    public class Game
    {
        private readonly StateMachine _gameStateMachine;
        
        public Game(IEnumerable<IObjectGenerator> objectGenerators)
        {
            _gameStateMachine = new StateMachine(
                new GameState(objectGenerators),
                new DefeatState()
                );
        }

        public void SetState(GameStates state)
        {
            switch (state)
            {
                case GameStates.Game:
                    _gameStateMachine.ChangeState<GameState>();
                    break;
                case GameStates.Defeat:
                    _gameStateMachine.ChangeState<DefeatState>();
                    break;
            }
        }
    }
}