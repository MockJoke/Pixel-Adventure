using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private BoxCollider2D playerCollider;
    [SerializeField] private SpriteRenderer charSprite;
    [SerializeField] private Animator animator;
    [SerializeField] private ParticleSystem dustEffect;
    [SerializeField] private ParticleSystem dashEffect;
    [SerializeField] private ParticleSystem jumpEffect;
    
    [Header("Movement")]
    [SerializeField] private float moveSpeed = 7f;
    
    [Header("Dash")]
    [SerializeField, Range(0f, 30f), Tooltip("Set as 0, if player can't dash")] private float dashSpeed = 21f;
    [SerializeField, Range(0f, 1f), Tooltip("Amount of time, player will remain in dashing")] private float dashDuration = 0.2f;
    [SerializeField, Range(0f, 5f), Tooltip("Duration bw two dashes")] private float dashCooldownDuration = 1f;
    
    [Header("Jump")]
    [SerializeField] private float jumpForce = 14f;
    [SerializeField] private float extraJumpForce = 10f;
    [SerializeField] private int maxAirJumpCnt = 1;
    [SerializeField] private LayerMask groundLayer;

    [Header("Wall Grab & Jump")]
    [SerializeField] private float grabCheckRadius = 0.24f;
    [SerializeField] private Vector2 grabRightOffset = new Vector2(0.16f, 0f);
    [SerializeField] private Vector2 grabLeftOffset = new Vector2(-0.16f, 0f);
    [SerializeField] private float wallSlideSpeed = 2.5f;
    [SerializeField] private Vector2 wallClimbForce = new Vector2(4f, 14f);
    [SerializeField] private Vector2 wallJumpForce = new Vector2(10.5f, 18f);
    
    private float dirX = 0f;
    private int airJumpCnt = 0;
    private bool isDashing = false;
    private float dashTimer = 0f;
    private float dashCoolDownTimer = 0f;
    private bool onRightWall = false;
    private bool onLeftWall = false;
    private bool grabbingWall = false;
    private bool jumpingFromWall = false;
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

        if (charSprite == null)
            charSprite = GetComponent<SpriteRenderer>();

        if (animator == null)
            animator = GetComponent<Animator>();
    }

    void Start()
    {
        airJumpCnt = maxAirJumpCnt;
    }

    void Update()
    {
        #region For Old Input System (Unused)
        // Game Paused
        // if (Time.timeScale == 0)
        //     return;
        
        // dirX = Input.GetAxisRaw("Horizontal");
        // Move();

        // if (Input.GetKeyDown(KeyCode.LeftShift) && dashSpeed > 0f)
        // {
        //     if (!isDashing && dashCoolDownTimer >= dashCooldownDuration)
        //     {
        //         isDashing = true;
        //         PlayDashEffect();
        //     }
        // }
        
        // dashCoolDownTimer += Time.fixedDeltaTime;
        //
        // if (isDashing)
        // {
        //     if (dashTimer >= dashDuration)
        //     {
        //         ResetDash();
        //     }
        //     else
        //     {
        //         dashTimer += Time.fixedDeltaTime;
        //         Dash();
        //     }
        // }
        
        // if (IsGrounded())
        // {
        //     airJumpCnt = 0;
        // }

        // if (Input.GetButton("Jump"))
        // {
        //     if (IsGrounded())
        //     {
        //         Jump(jumpForce);
        //     }
        //     else if (Input.GetButtonDown("Jump") && airJumpCnt < maxAirJumpCnt)
        //     {
        //         Jump(extraJumpForce);
        //         airJumpCnt++;
        //     }
        // }
        #endregion

        UpdateAnimationState();
    }

    private void FixedUpdate()
    {
        Move();
        
        if (IsGrounded())
        {
            airJumpCnt = 0;
        }
        
        dashCoolDownTimer += Time.fixedDeltaTime;

        if (isDashing)
        {
            if (dashTimer >= dashDuration)
            {
                ResetDash();
            }
            else
            {
                dashTimer += Time.fixedDeltaTime;
                Dash();
            }
        }
    }

    public void SetData(CharacterDataSO.CharacterData charData)
    {
        charSprite.sprite = charData.charSprite;
        animator.runtimeAnimatorController = charData.animatorController;

        moveSpeed = charData.moveSpeed;
        jumpForce = charData.jumpForce;
        extraJumpForce = charData.extraJumpForce;
        maxAirJumpCnt = charData.maxAirJumpCnt;

        dashSpeed = charData.dashSpeed;
        dashDuration = charData.dashDuration;
        dashCooldownDuration = charData.dashCooldownDuration;
    }

    #region Input Actions

    public void ReadHorizontalMovement(InputAction.CallbackContext context)
    {
        dirX = context.ReadValue<float>();
    }

    public void ReadJump(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            if (IsGrounded())
            {
                Jump(jumpForce);
            }
            else if (context.performed && airJumpCnt < maxAirJumpCnt)
            {
                Jump(extraJumpForce);
                airJumpCnt++;
            }
        }
    }

    public void ReadDash(InputAction.CallbackContext context)
    {
        if (context.performed && dashSpeed > 0f)
        {
            if (!isDashing && dashCoolDownTimer >= dashCooldownDuration)
            {
                isDashing = true;
                PlayDashEffect();
            }
        }
    }
    #endregion

    private void Move()
    {
        rb.velocity = new Vector2(dirX * moveSpeed, rb.velocity.y);
    }

    private void Dash()
    {
        rb.velocity = charSprite.flipX ? new Vector2(-1f * dashSpeed, rb.velocity.y) : new Vector2(1f * dashSpeed, rb.velocity.y);
    }

    private void ResetDash()
    {
        isDashing = false;
        dashCoolDownTimer = 0f;
        dashTimer = 0f;
        rb.velocity = Vector2.zero;
    }

    private void Jump(float jumpF)
    {
        PlayParticleEffect(jumpEffect);
        AudioManager.Instance.PlaySound(AudioType.characterJump);
        rb.velocity = new Vector2(rb.velocity.x, jumpF);
    }

    private void UpdateAnimationState()
    {
        MovementState state;
        
        if (dirX > 0f)
        {
            PlayParticleEffect(dustEffect);
            state = MovementState.running;
            charSprite.flipX = false;
        }
        else if (dirX < 0f) 
        {
            PlayParticleEffect(dustEffect);
            state = MovementState.running;
            charSprite.flipX = true;
        }
        else
        {
            state = MovementState.idle;
        }
        
        if (rb.velocity.y > .1f)
        {
            state = airJumpCnt == 0 ? MovementState.jumping : MovementState.doubleJumping;
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

    private bool isOnRightWall()
    {
        return Physics2D.OverlapCircle((Vector2)transform.position + grabRightOffset, grabCheckRadius, groundLayer);
    }
    
    private bool isOnLeftWall()
    {
        return Physics2D.OverlapCircle((Vector2)transform.position + grabLeftOffset, grabCheckRadius, groundLayer);
    }
    
    private void PlayParticleEffect(ParticleSystem effect) 
    {
        effect.Play();
    }

    private void PlayDashEffect()
    {
        Vector3 dashPos = dashEffect.transform.localPosition;

        dashEffect.transform.localPosition = charSprite.flipX ? new Vector3(1.2f, dashPos.y, dashPos.z) : new Vector3(-1.2f, dashPos.y, dashPos.z);
        
        PlayParticleEffect(dashEffect);
    }

    public void ResetFlipping()
    {
        charSprite.flipX = false;
    }
    
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        
        Gizmos.DrawWireSphere((Vector2)transform.position + grabRightOffset, grabCheckRadius);
        Gizmos.DrawWireSphere((Vector2)transform.position + grabLeftOffset, grabCheckRadius);
    }
}