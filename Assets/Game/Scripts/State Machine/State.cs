public class StateData{
    
}

public interface IState
{
    void Enter(StateData stateData = null);
    void Update();
    void FixedUpdate();
    void Exit();
}

public abstract class State<TStateMachine> : IState
{
    protected TStateMachine _context;
    public State(TStateMachine context)
    {
        _context = context;
    }

   
    public abstract void Enter(StateData stateData = null);

    public abstract void Update();
    public virtual void FixedUpdate(){}

    public abstract void Exit();
}