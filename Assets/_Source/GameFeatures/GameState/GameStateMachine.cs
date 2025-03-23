using System.Collections.Generic;
using Core.Generation;
using Core.StateMachine;
using GameFeatures.Menu;
using GameFeatures.PlayerInput;
using GameFeatures.TowerDefence;
using GameFeatures.WorkProgress;

namespace GameFeatures.GameState
{
    public enum GameStates
    {
        Menu = 0,
        Game = 1,
        Defeat = 2
    }
    
    public class GameStateMachine
    {
        private readonly StateMachine _gameStateMachine;
        
        public GameStateMachine(IEnumerable<IObjectGenerator> objectGenerators, IEnumerable<IResettable> resettable,
            EntityResetter entityResetter, ExpirationCounter expirationCounter, MenuPanelView menuView, InputListener inputListener)
        {
            _gameStateMachine = new StateMachine(
                new MenuState(menuView),
                new GameState(objectGenerators, resettable, entityResetter, expirationCounter, inputListener),
                new DefeatState()
                );
        }

        public void SetState(GameStates state)
        {
            switch (state)
            {
                case GameStates.Menu:
                    _gameStateMachine.ChangeState<MenuState>();
                    break;
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