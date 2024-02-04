using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private Rigidbody2D rb;

    [SerializeField] private BoxCollider2D playerCollider;
    [SerializeField] private SpriteRenderer sprite;
    [SerializeField] private Animator animator;
    [SerializeField] private LayerMask groundLayer;

    [Header("Movement")]
    [SerializeField] private float moveSpeed = 7f;
    [SerializeField] private float jumpForce = 14f;
    private float dirX = 0f;
    private static readonly int animState = Animator.StringToHash("state");

    private enum MovementState
    {
        idle,
        running,
        jumping,
        falling
    }

    void Awake()
    {
        if (rb == null)
            rb = GetComponent<Rigidbody2D>();

        if (playerCollider == null)
            playerCollider = GetComponent<BoxCollider2D>();

        if (sprite == null)
            sprite = GetComponent<SpriteRenderer>();

        if (animator == null)
            animator = GetComponent<Animator>();
    }

    void Update()
    {
        dirX = Input.GetAxisRaw("Horizontal");
        Move();

        if (Input.GetButtonDown("Jump") && IsGrounded())
        {
            Jump();
        }

        UpdateAnimationState();
    }

    private void Move()
    {
        rb.velocity = new Vector2(dirX * moveSpeed, rb.velocity.y);
    }

    private void Jump()
    {
        AudioManager.instance.PlaySound(AudioType.characterJump);
        rb.velocity = new Vector2(rb.velocity.x, jumpForce);
    }

    private void UpdateAnimationState()
    {
        MovementState state;

        if (dirX > 0f)
        {
            state = MovementState.running;
            sprite.flipX = false;
        }
        else if (dirX < 0f)
        {
            state = MovementState.running;
            sprite.flipX = true;
        }
        else
        {
            state = MovementState.idle;
        }

        if (rb.velocity.y > .1f)
        {
            state = MovementState.jumping;
        }
        else if (rb.velocity.y < -.1f)
        {
            state = MovementState.falling;
        }

        animator.SetInteger(animState, (int)state);
    }

    private bool IsGrounded()
    {
        Bounds bounds = playerCollider.bounds;
        return Physics2D.BoxCast(bounds.center, bounds.size, 0f, Vector2.down, .1f, groundLayer);
    }
}        