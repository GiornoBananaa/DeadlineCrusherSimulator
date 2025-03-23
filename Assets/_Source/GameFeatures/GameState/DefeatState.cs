using Core.StateMachine;
using GameFeatures.Menu;

namespace GameFeatures.GameState
{
    public class DefeatState : IState
    {
        private readonly DefeatPanelView _defeatPanelView;
        private StateMachine _owner;
        
        public DefeatState(DefeatPanelView defeatPanelView)
        {
            _defeatPanelView = defeatPanelView;
        }

        public void SetOwner(StateMachine owner)
        {
            _owner = owner;
        }

        public void Enter()
        {
            _defeatPanelView.Show();
            _defeatPanelView.OnReplayPressed += OnPlayPressed;
        }

        public void Exit()
        {
            _defeatPanelView.OnReplayPressed -= OnPlayPressed;
            _defeatPanelView.Hide();
        }

        private void OnPlayPressed()
        {
            _owner.ChangeState<MenuState>();
        }
    }
}