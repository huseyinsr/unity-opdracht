using UnityEngine;

public class WallJumpScript : MonoBehaviour
{
    public float wallJumpUpForce = 10f;       // Vertical force when wall jumping
    public float wallCheckDistance = 1f;      // Distance to check for wall
    public float wallSlidingSpeed = 0.5f;     // Speed to slide down the wall when not jumping
    public LayerMask whatIsWall;              // Layer for the wall
    public LayerMask whatIsGround;            // Layer for the ground

    private Rigidbody rb;
    private CapsuleCollider col;

    private bool isWallJumping;
    private bool isTouchingWall;
    private bool isGrounded;
    private bool isWallSliding;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        col = GetComponent<CapsuleCollider>();
    }

    private void Update()
    {
        isGrounded = IsGrounded();
        isTouchingWall = IsTouchingWall();

        if (isTouchingWall && !isGrounded)
        {
            if (!isWallSliding)
                StartWallSlide();

            if (Input.GetKeyDown(KeyCode.Space))
            {
                WallJumpUpOnly();
            }
        }
        else
        {
            if (isWallSliding)
                StopWallSlide();
        }
    }

    private void FixedUpdate()
    {
        if (isWallSliding)
        {
            WallSlide();
        }
    }

    private bool IsGrounded()
    {
        return Physics.Raycast(transform.position, Vector3.down, 0.6f, whatIsGround);
    }

    private bool IsTouchingWall()
    {
        RaycastHit hit;
        return
            Physics.Raycast(transform.position, Vector3.left, out hit, wallCheckDistance, whatIsWall) ||
            Physics.Raycast(transform.position, Vector3.right, out hit, wallCheckDistance, whatIsWall) ||
            Physics.Raycast(transform.position, Vector3.forward, out hit, wallCheckDistance, whatIsWall) ||
            Physics.Raycast(transform.position, Vector3.back, out hit, wallCheckDistance, whatIsWall);
    }

    private void StartWallSlide()
    {
        isWallSliding = true;
    }

    private void StopWallSlide()
    {
        isWallSliding = false;
    }

    private void WallSlide()
    {
        if (!isWallJumping)
        {
            rb.velocity = new Vector3(rb.velocity.x, Mathf.Clamp(rb.velocity.y, -wallSlidingSpeed, float.MaxValue), rb.velocity.z);
        }
    }

    private void WallJumpUpOnly()
    {
        isWallJumping = true;
        rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z); // Reset vertical speed
        rb.AddForce(Vector3.up * wallJumpUpForce, ForceMode.Impulse);
        Invoke(nameof(ResetWallJump), 0.2f); // Prevent sticking to wall for a moment
    }

    private void ResetWallJump()
    {
        isWallJumping = false;
    }
}
