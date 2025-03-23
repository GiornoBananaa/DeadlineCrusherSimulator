using Core.StateMachine;

namespace GameFeatures.GameState
{
    public class DefeatState : IState
    {
        private StateMachine _owner;
        
        public DefeatState()
        {
            
        }
        
        public void SetOwner(StateMachine owner)
        {
            _owner = owner;
        }
        
        public void Enter()
        {
            
        }

        public void Exit()
        {
            
        }
    }
}