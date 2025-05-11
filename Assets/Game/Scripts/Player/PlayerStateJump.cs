using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerStateJump : State<PlayerStateMachine>
{
    public PlayerStateJump(PlayerStateMachine context) : base(context)
    {
    }

    public override void Enter(StateData stateData = null)
    {
        Vector3 currentVelocity = _context.Owner.PlayerRB.velocity;
        _context.Owner.PlayerRB.velocity = new Vector3(currentVelocity.x, _context.Owner.JumpForce, 0);
        
       
       
    }

    public override void Update()
    {
       
    }

    public override void FixedUpdate()
    {
        
    }

    public override void Exit()
    {
        
    }
}
