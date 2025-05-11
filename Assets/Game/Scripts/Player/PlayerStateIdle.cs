using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateIdle : State<PlayerStateMachine>
{
   
    public override void Enter(StateData stateData = null)
    {
       
    }

    public override void Update()
    {
        
    }


    public override void Exit()
    {
        
    }

    public PlayerStateIdle(PlayerStateMachine context) : base(context)
    {
    }
}
