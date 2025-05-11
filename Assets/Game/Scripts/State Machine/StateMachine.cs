using System;
using System.Collections.Generic;

public interface IStateMachine
{
    void SetState(Enum state, StateData stateData = null);
    void Update();
    void FixedUpdate();
    void Exit();
    bool IsState(Enum state);
}

public abstract class StateMachine<TStateMachine, TEntity> : IStateMachine
{
    private TEntity _owner;
    private State<TStateMachine> _previousState;
    private State<TStateMachine> _currentState;
    private List<State<TStateMachine>> _states = new List<State<TStateMachine>>();
    
    public Action<Enum> OnStateChanged;
    public TEntity Owner => _owner;
    public StateMachine(TEntity owner)
    {
        _owner = owner;
    }

    protected void AddState(State<TStateMachine> state)
    {
        _states.Add(state);
    }
    public void SetState(Enum state, StateData stateData = null)
    {
        _previousState = _currentState;
        if(_previousState != null) _previousState.Exit();

        _currentState = _states[Convert.ToInt32(state)];
        _currentState?.Enter(stateData);
        
        OnStateChanged?.Invoke(state);
    }

    public virtual void Update()
    {
        if(_currentState != null) _currentState.Update();
    }

    public void FixedUpdate()
    {
        if(_currentState != null) _currentState.FixedUpdate();
    }

    public void Exit()
    {
        if(_currentState == null) return;
        _currentState.Exit();
        _currentState = null;
    }

    public bool IsState(Enum state)
    {
        return _currentState == _states[Convert.ToInt32(state)];
    }
}
