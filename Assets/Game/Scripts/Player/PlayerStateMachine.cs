public enum PlayerStateType
{
    Idle,
    Move,
    Jump
}
public class PlayerStateMachine : StateMachine<PlayerStateMachine, Player>
{
    public PlayerStateMachine(Player owner) : base(owner)
    {
        AddState(new PlayerStateIdle(this));
        AddState(new PlayerStateMove(this));
        AddState(new PlayerStateJump(this));
    }
    
}