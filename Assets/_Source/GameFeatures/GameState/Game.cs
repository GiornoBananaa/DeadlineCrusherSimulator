using System.Collections.Generic;
using Core.Generation;

namespace GameFeatures.GameState
{
    public enum GameStates
    {
        Game = 0,
        Defeat = 1
    }
    
    public class Game
    {
        private readonly Core.StateMachine.StateMachine _gameStateMachine;
        
        public Game(IEnumerable<IObjectGenerator> objectGenerators)
        {
            _gameStateMachine = new Core.StateMachine.StateMachine(
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