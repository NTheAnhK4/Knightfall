using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeEnemySM : EnemySM
{
   
    public MeleeEnemySM(Enemy owner) : base(owner)
    {
        AddState(new ESChase(this));
        AddState(new ESPatrol(this));
        AddState(new ESMeleeAttack(this));
    }
}
