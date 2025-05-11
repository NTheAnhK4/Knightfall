using System.Collections;
using System.Collections.Generic;
using Pathfinding;
using UnityEngine;

public class ESPatrolData : EnemyPathFindingData
{
    public Transform[] Targets;
    //Time delay at each point
    public float Delay = 0;
   
    
}
public class ESPatrol : EnemyPathFinding
{
    private Transform[] _targets;
    private int _index;
    private float _delay;
    private float _cooldown;
    public override void Enter(StateData stateData = null)
    {
        
        if (stateData != null && stateData is ESPatrolData patrolData)
        {
            _targets = patrolData.Targets;
            _delay = patrolData.Delay;
            _index = 0;
            _cooldown = _delay;
            if (_targets != null && _targets.Length > 0) _target = _targets[_index];
        }
        base.Enter(stateData);
    }

    public override void Update()
    {
        base.Update();
       
        if (_cooldown >= _delay) return;
        _cooldown += Time.deltaTime;
    }
    
    public override void FixedUpdate()
    {
        if (!CanNavigation())
        {
            _enemyData.Rigidbody2D.velocity = Vector2.zero;
            return;
        }
        Navigation();
    }

    protected override bool CanNavigation()
    {
        if (_path == null) return false;
        if (IsPathComplete()) return false;
        if (_cooldown < _delay) return false;
        return true;
      
    }

    protected override bool IsPathComplete()
    {
        if (base.IsPathComplete())
        {
            if (_targets == null || _targets.Length <= 0) return true;
            _index = (_index + 1) % _targets.Length;
            _target = _targets[_index];
            _path = null;
            if (_enemyData.IsFlyEnemy) _cooldown = 0f;
            else
            {
                CheckOnGround();
                if (_isGrounded) _cooldown = 0f;
            }
            
            UpdatePath();
        }

        return false;
    }

    public ESPatrol(EnemySM context) : base(context)
    {
    }
}
