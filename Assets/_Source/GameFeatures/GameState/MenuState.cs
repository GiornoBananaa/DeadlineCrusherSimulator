using Core.StateMachine;
using GameFeatures.Menu;

namespace GameFeatures.GameState
{
    public class MenuState : IState
    {
        private readonly MenuPanelView _menuView;
        private StateMachine _owner;
        
        public MenuState(MenuPanelView menuView)
        {
            _menuView = menuView;
        }

        public void SetOwner(StateMachine owner)
        {
            _owner = owner;
        }

        public void Enter()
        {
            _menuView.Show();
            _menuView.OnPlayPressed += OnPlayPressed;
        }

        public void Exit()
        {
            _menuView.OnPlayPressed -= OnPlayPressed;
            _menuView.Hide();
        }

        private void OnPlayPressed()
        {
            _owner.ChangeState<GameState>();
        }
    }
}