using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EnemyStateType
{
    Chase,
    Patrol,
    Attack
    
}
public class EnemySM : StateMachine<EnemySM, Enemy>
{
    public EnemySM(Enemy owner) : base(owner)
    {
       
    }
}
