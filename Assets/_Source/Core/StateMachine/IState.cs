namespace Core.StateMachine
{
    public interface IState
    {
        void SetOwner(StateMachine owner);
        void Enter();
        void Exit();
    }
}