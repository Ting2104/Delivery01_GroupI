using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMove : MonoBehaviour
{
    [SerializeField]
    private float Speed = 5.0f;
    private float jumpForce = 5.2f;

    Rigidbody2D _rigidbody;
    private float _horizontalDir; // Horizontal move direction value [-1, 1]
    private bool _isGrounded = false;
    private bool _isTouchingCeiling = false;
    private bool _isTouchingWallL;
    private bool _isTouchingWallR;
    private int _remainingJumps; // Jumps remaining
    private const int MaxJumps = 2; // Max Jumps

    [SerializeField]
    private LayerMask groundLayer; 
    [SerializeField]
    private Transform groundCheck; 
    [SerializeField]
    private Transform ceilingCheck;
    [SerializeField]
    private Transform wallCheckL;
    [SerializeField]
    private Transform wallCheckR;
    private float checkRadius = 0.2f; // Detection Radius

    void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        Vector2 velocity = _rigidbody.linearVelocity;
        velocity.x = _horizontalDir * Speed;
        _rigidbody.linearVelocity = velocity;

        _isGrounded = Physics2D.OverlapCircle(groundCheck.position, checkRadius, groundLayer);
        _isTouchingCeiling = Physics2D.OverlapCircle(ceilingCheck.position, checkRadius, groundLayer);
        _isTouchingWallL = Physics2D.OverlapCircle(wallCheckL.position, checkRadius, groundLayer);
        _isTouchingWallR = Physics2D.OverlapCircle(wallCheckR.position, checkRadius, groundLayer);

        if (_isGrounded)
        {
            _remainingJumps = MaxJumps;
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "PowerUp") // If touches the powerup get jump boost
        {
            jumpForce = jumpForce + 3;
            Destroy(other.gameObject);
        }
    }

    // NOTE: InputSystem: "move" action becomes "OnMove" method
    void OnMove(InputValue value)
    {
        // Read value from control, the type depends on what
        // type of controls the action is bound to
        var inputVal = value.Get<Vector2>();
        if (_isTouchingWallL && inputVal.x < 0)
        {
            _horizontalDir = 0;
        }
        else if (_isTouchingWallR && inputVal.x > 0)
        {
            _horizontalDir = 0;
        }
        else
        {
            _horizontalDir = inputVal.x; 
        }
    }

    void OnJumpStarted(InputValue value)
    {
        if (value.isPressed)
        {
            if (_remainingJumps > 1)
            {
                _rigidbody.linearVelocity = new Vector2(_rigidbody.linearVelocity.x, jumpForce);
                _remainingJumps--; 
            }
        }
    }

    void OnDrawGizmos() // Visual Debug lines
    {
        if (groundCheck != null)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(groundCheck.position, checkRadius);
        }
        if (ceilingCheck != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(ceilingCheck.position, checkRadius);
        }
        if (wallCheckL != null)
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(wallCheckL.position, checkRadius);
        }
        if (wallCheckR != null)
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(wallCheckR.position, checkRadius);
        }
    }
}
