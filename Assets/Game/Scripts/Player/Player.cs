

using System;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : Entity
{
    private PlayerInputActions _playerInputActions;
    private Rigidbody2D _playerRB;
    private float _speed = 5f;
    private float _jumpForce = 7f;
    [SerializeField] private int _jumpCount = 0;

    [SerializeField]  private LayerMask _groundLayer;
    public Rigidbody2D PlayerRB => _playerRB;
    public float Speed => _speed;
    public float JumpForce => _jumpForce;
    [HideInInspector] public bool IsFacingRight;
    
    private void Awake()
    {
        _playerInputActions = new PlayerInputActions();
        _playerInputActions.Player.Enable();
        _playerInputActions.Player.Jump.performed += OnPlayerJump;
        
        _playerRB = GetComponent<Rigidbody2D>();
        
        StateMachine = new PlayerStateMachine(this);
        StateMachine.SetState(PlayerStateType.Idle);

        _groundLayer = LayerMask.GetMask("Ground");
    }

   

    protected override void Update()
    {
        Debug.DrawRay(transform.position, Vector2.down, Color.green);
        float horizontal = GetHorizontalInput();
        if (horizontal != 0) IsFacingRight = horizontal > 0;

      
        if(horizontal != 0 && !StateMachine.IsState(PlayerStateType.Move)) StateMachine.SetState(PlayerStateType.Move, new PlayerStateMoveData(){CurrentHorizontal = horizontal});
        if(horizontal == 0 && !StateMachine.IsState(PlayerStateType.Idle)) StateMachine.SetState(PlayerStateType.Idle);
        
        StateMachine.Update();
    }

    private void OnPlayerJump(InputAction.CallbackContext context)
    {
        CheckOnGround();
        if (_jumpCount >= 2) return;
        _jumpCount++;
        StateMachine.SetState(PlayerStateType.Jump);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        CheckOnGround();
    }

    void CheckOnGround()
    {
        Vector2 origin = transform.position;
        
        RaycastHit2D hit = Physics2D.Raycast(origin, Vector2.down, 1.1f, _groundLayer);
     
        if (hit.collider != null && hit.collider.gameObject.tag.Equals("Ground")) _jumpCount = 0;
    }

    

    public float GetHorizontalInput()
    {
        return _playerInputActions.Player.Movement.ReadValue<float>();
    }
    
    
    
    
}
