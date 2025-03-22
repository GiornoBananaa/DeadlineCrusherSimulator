using GameFeatures.GameState;
using UnityEngine;
using Zenject;

namespace GameFeatures.Installing
{
    public class Bootstrapper : MonoBehaviour
    {
        private GameStateMachine _gameStateMachine;
        
        [Inject]
        public void Construct(GameStateMachine gameStateMachine)
        {
            _gameStateMachine = gameStateMachine;
        }
        
        public void Start()
        {
            _gameStateMachine.SetState(GameStates.Game);
        }
    }
}