using UnityEngine;
using UnityEngine.InputSystem;

            /////////////// INFORMATION ///////////////
// This script automatically adds a Rigidbody2D and a CapsuleCollider2D component in the inspector.
// The following components are needed: Player Input

[RequireComponent(typeof(Rigidbody2D), typeof(CapsuleCollider2D))]
public class TopDownMovement : MonoBehaviour
{
    public float maxSpeed = 7;
    
    public bool controlEnabled { get; set; } = true;    // You can edit this variable from Unity Events
    
    private Vector2 moveInput;
    private Rigidbody2D rb;
    private Animator animator;
    private const string _horizontal = "Horizontal";
    private const string _vertical = "Vertical";
    private const string _lastHorizontal = "LastHorizontal";
    private const string _lastVertical = "LastVertical";
    
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        
        // Set gravity scale to 0 so player won't "fall" 
        rb.gravityScale = 0;
    }

    private void Update()
    {
        Vector2 movement = moveInput;
        Vector2 idleVector2 = new Vector2(0, 0);
        animator.SetFloat(_horizontal, moveInput.x);
        animator.SetFloat(_vertical, moveInput.y);

        if (movement != idleVector2)
        {
            animator.SetFloat(_lastHorizontal, moveInput.x);
            animator.SetFloat(_lastVertical, moveInput.y);
        }
    }
    
    private void FixedUpdate()
    {
        // Set velocity based on direction of input and maxSpeed
        if (controlEnabled)
        {
            rb.linearVelocity = moveInput.normalized * maxSpeed;
        }
            
        else
        {
            rb.linearVelocity = Vector2.zero;
        }
        
        // Write code for walking animation here. (Suggestion: send your current velocity into the Animator for both the x- and y-axis.)
    }
    
    // Handle Move-input
    // This method can be triggered through the UnityEvent in PlayerInput
    public void OnMove(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>().normalized;
        
    }
}
