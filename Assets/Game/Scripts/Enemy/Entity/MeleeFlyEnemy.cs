using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeFlyEnemy : Enemy
{
    protected override void Awake()
    {
        base.Awake();
        StateMachine = new MeleeEnemySM(this);
        if (_rigidbody2D != null) _rigidbody2D.gravityScale = 0;
    }

    protected override ESChaseData GetEnemyChaseData()
    {
        ESChaseData result = base.GetEnemyChaseData();
        result.IsFlyEnemy = true;
        return result;
    }

    protected override ESPatrolData GetEnemyStatePatrol()
    {
        ESPatrolData result = base.GetEnemyStatePatrol();
        result.IsFlyEnemy = true;
        return result;
    }
}
