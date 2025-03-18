using Systems.GameStateSystem;
using UnityEngine;
using Zenject;

namespace Systems.InstallingSystem
{
    public class Bootstrapper : MonoBehaviour
    {
        private Game _game;
        
        [Inject]
        public void Construct(Game game)
        {
            _game = game;
        }
        
        public void Start()
        {
            _game.SetState(GameStates.Game);
        }
    }
}