using System;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private BoxCollider2D playerCollider;
    [SerializeField] private SpriteRenderer sprite;
    [SerializeField] private Animator animator;
    [SerializeField] private ParticleSystem dust;
    
    [Header("Movement")]
    [SerializeField] private float moveSpeed = 7f;
    [SerializeField] private float jumpForce = 16f;
    [SerializeField] private float doubleJumpForce = 10f;
    [SerializeField] private int extraJumps = 1;
    [SerializeField] private LayerMask groundLayer;

    private int jumpCount = 0;
    private float dirX = 0f;
    private static readonly int animState = Animator.StringToHash("state");

    private enum MovementState
    {
        idle = 0,
        running = 1,
        jumping = 2,
        falling = 3,
        doubleJumping = 4
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

        if (dust == null)
            dust = GetComponentInChildren<ParticleSystem>();
    }

    void Start()
    {
        jumpCount = extraJumps;
    }

    void Update()
    {
        // Game Paused
        if (Time.timeScale == 0)
            return;
        
        dirX = Input.GetAxisRaw("Horizontal");
        Move();

        if (IsGrounded())
        {
            jumpCount = extraJumps;
        }

        if (Input.GetButtonDown("Jump") && IsGrounded())
        {
            Jump(jumpForce);
        }
        else if (Input.GetButtonDown("Jump") && jumpCount > 0)
        {
            Jump(doubleJumpForce);
            jumpCount--;
        }

        UpdateAnimationState();
    }

    private void Move()
    {
        rb.velocity = new Vector2(dirX * moveSpeed, rb.velocity.y);
    }

    private void Jump(float jumpF)
    {
        CreateDust();
        AudioManager.Instance.PlaySound(AudioType.characterJump);
        rb.velocity = new Vector2(rb.velocity.x, jumpF);
    }

    private void UpdateAnimationState()
    {
        MovementState state;

        if (dirX > 0f)
        {
            CreateDust();
            state = MovementState.running;
            sprite.flipX = false;
        }
        else if (dirX < 0f) 
        {
            CreateDust();
            state = MovementState.running;
            sprite.flipX = true;
        }
        else
        {
            state = MovementState.idle;
        }

        if (rb.velocity.y > .1f)
        {
            // if (jumpCount > 0)
            // {
                state = MovementState.jumping;
            // }
            // else
            // {
            //     state = MovementState.doubleJumping;
            // }
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
    
    private void CreateDust() 
    {
        dust.Play();
    }

    public void ResetFlipping()
    {
        sprite.flipX = false;
    }
}