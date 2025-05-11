using UnityEngine;

public class PlayerStateMoveData : StateData
{
    public float CurrentHorizontal;
}
public class PlayerStateMove : State<PlayerStateMachine>
{
   
    private float _horizontal;
    public PlayerStateMove(PlayerStateMachine context) : base(context)
    {
    }

    public override void Enter(StateData stateData = null)
    {

        if (stateData != null && stateData is PlayerStateMoveData moveData)
        {
            _horizontal = moveData.CurrentHorizontal;
        }
    }

    public override void Update()
    {
        _horizontal = _context.Owner.GetHorizontalInput();

    }

    public override void FixedUpdate()
    {
        
        _context.Owner.transform.Translate(new Vector3(_horizontal * _context.Owner.Speed,0,0) * Time.fixedDeltaTime);
       
    }

    public override void Exit()
    {
        
    }
}