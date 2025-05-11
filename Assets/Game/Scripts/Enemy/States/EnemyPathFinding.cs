using System.Collections;
using System.Collections.Generic;
using Pathfinding;
using UnityEngine;

public class EnemyPathFindingData : StateData
{
    public float PathUpdateSeconds;

    public float MoveSpeed;
    public float NextWayPointDistance;
    public float JumpNodeHeightRequirement;
    public float JumpForce;
    public Seeker Seeker;
    public Rigidbody2D Rigidbody2D;
    public bool IsFlyEnemy = false;
}
public class EnemyPathFinding : State<EnemySM>
{
    protected EnemyPathFindingData _enemyData;
    protected Path _path;
    private int _currentWaypoint;
    protected bool _isGrounded;
    private float _coolDown;
    protected Transform _target;
    public EnemyPathFinding(EnemySM context) : base(context)
    {
       
    }

    public override void Enter(StateData stateData = null)
    {
        if (stateData != null && stateData is EnemyPathFindingData pathFindingData)
        {
            _enemyData = pathFindingData;
            UpdatePath();
        }

        _currentWaypoint = 0;
        _isGrounded = false;
        _coolDown = 0f;
    }

    protected void UpdatePath()
    {
        if (!_enemyData.Seeker.IsDone() || _target == null) return;
        _coolDown = 0f;
        _enemyData.Seeker.StartPath(_context.Owner.transform.position, _target.position, OnPathComplete);
    }

    private void OnPathComplete(Path p)
    {
        if (p.error) return;
        _path = p;
        _currentWaypoint = 0;
    }

    public override void Update()
    {
        if (_enemyData == null)
        {
            return;
        }
        _coolDown += Time.deltaTime;
        if(_coolDown >= _enemyData.PathUpdateSeconds) UpdatePath();
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

    protected virtual bool CanNavigation()
    {
        if (_path == null) return false;
        if (IsPathComplete()) return false;
        return true;
    }

    protected void Navigation()
    {
        if (_path == null) return;
        
        Vector2 ownerPosition = _context.Owner.transform.position;
       
       
        
        Vector2 direction = ((Vector2)_path.vectorPath[_currentWaypoint] - ownerPosition).normalized;

        if (_enemyData.IsFlyEnemy)
        {
            Vector2 targetVelocity = direction * _enemyData.MoveSpeed;
            _enemyData.Rigidbody2D.velocity = Vector2.Lerp(
                _enemyData.Rigidbody2D.velocity,
                targetVelocity,
                0.1f
            );

        }
        else
        {
            CheckOnGround();
            if (_isGrounded && direction.y >= _enemyData.JumpNodeHeightRequirement)
            {
                _enemyData.Rigidbody2D.AddForce(Vector2.up * _enemyData.JumpForce,ForceMode2D.Impulse);
            }

            var velocity = _enemyData.Rigidbody2D.velocity;
            Vector2 moveDirection = new Vector2(direction.x * _enemyData.MoveSpeed, velocity.y);

            _enemyData.Rigidbody2D.velocity = Vector2.Lerp(velocity, moveDirection, 0.1f);
            
        }
        
        SetNextWaypoint();
    }

    protected void CheckOnGround()
    {
        Vector2 ownerPosition = _context.Owner.transform.position;
        _isGrounded = Physics2D.Raycast(ownerPosition, Vector2.down, _context.Owner.GetComponent<Collider2D>().bounds.extents.y + 0.1f, LayerMask.GetMask("Ground"));
    }
    private void SetNextWaypoint()
    {
        float distance = Vector2.Distance(_context.Owner.transform.position, _path.vectorPath[_currentWaypoint]);
        if (distance < _enemyData.NextWayPointDistance)  _currentWaypoint++;
        
    }
    protected virtual bool IsPathComplete()
    {
     
        return _currentWaypoint >= _path.vectorPath.Count;
    }

    public override void Exit()
    {
        if (_enemyData != null)
        {
            _enemyData.Rigidbody2D.velocity = Vector2.zero;
        }
    }
}
