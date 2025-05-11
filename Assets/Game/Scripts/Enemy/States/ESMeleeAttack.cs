using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ESMeleeAttack : State<EnemySM>
{
   

    // Update is called once per frame
    public override void Enter(StateData stateData = null)
    {
        Debug.Log("Enter state attack");
    }

    public override void Update()
    {
        
    }


    public override void Exit()
    {
        
    }

    public ESMeleeAttack(MeleeEnemySM context) : base(context)
    {
    }
}
