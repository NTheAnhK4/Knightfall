using System;
using System.Collections;
using System.Collections.Generic;
using Pathfinding;
using UnityEngine;
[RequireComponent(typeof(Seeker))]
public class Enemy : Entity
{
    [Header("Patrol")]
    public Transform[] Targets;
    public float Delay = 3f;
    public float Vision = 10f;
    
  
    [Header("Chase")] public Transform Player;
    public float ActivateDistance = 50f;
    public float PathUpdateSeconds = .5f;

    public float MoveSpeed = 3f;
    public float NextWayPointDistance = 3f;
    public float JumpNodeHeightRequirement = .8f;
    public float JumpForce = 10f;

    [Header("Attack")] public float AttackRange = 2f;
    private Seeker _seeker;
    protected Rigidbody2D _rigidbody2D;
    
    protected virtual void Awake()
    {
       
        LoadComponent();
        
    }

    private void Start()
    {
        StateMachine.SetState(EnemyStateType.Patrol, GetEnemyStatePatrol());
       
    }

    private void LoadComponent()
    {
        _seeker = GetComponent<Seeker>();
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }

    protected override void Update()
    {
        var position = transform.position;
        Vector2 direction = (Player.transform.position - position);
        
        RaycastHit2D ray = Physics2D.Raycast(position, direction, Vision, LayerMask.GetMask("Ground") | LayerMask.GetMask("Player"));
        
        
        if (ray.collider != null && ray.collider.transform.tag.Equals("Player"))
        {
            if (IsInAttackRange(ray.collider.transform))
            {
                if(!StateMachine.IsState(EnemyStateType.Attack)) StateMachine.SetState(EnemyStateType.Attack);
            }
            else
            {
                if(!StateMachine.IsState(EnemyStateType.Chase)) StateMachine.SetState(EnemyStateType.Chase, GetEnemyChaseData());
            }
        }
        else
        {
            if(!StateMachine.IsState(EnemyStateType.Patrol)) StateMachine.SetState(EnemyStateType.Patrol, GetEnemyStatePatrol());
        }
            
        
        base.Update();
        
    }

    private bool IsInAttackRange(Transform target)
    {
        return Vector2.Distance(target.position, transform.position) <= AttackRange;
    }

    protected virtual ESChaseData GetEnemyChaseData()
    {
        return new ESChaseData()
        {
            JumpForce = JumpForce,
            JumpNodeHeightRequirement = JumpNodeHeightRequirement,
            MoveSpeed = MoveSpeed,
            NextWayPointDistance = NextWayPointDistance,
            PathUpdateSeconds = PathUpdateSeconds,
            Rigidbody2D = _rigidbody2D,
            Seeker = _seeker,
            Target = Player,
            
        };
    }

    protected virtual ESPatrolData GetEnemyStatePatrol()
    {
        return new ESPatrolData()
        {
            Delay = Delay,
            JumpForce = JumpForce,
            JumpNodeHeightRequirement = JumpNodeHeightRequirement,
            MoveSpeed = MoveSpeed,
            NextWayPointDistance = NextWayPointDistance,
            PathUpdateSeconds = PathUpdateSeconds,
            Rigidbody2D = _rigidbody2D,
            Seeker = _seeker,
            Targets = Targets,
          
        };
    }
}
