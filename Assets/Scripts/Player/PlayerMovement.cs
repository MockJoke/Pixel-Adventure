using System.Collections;
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
    [SerializeField] private bool canWallGrab = false;
    [SerializeField] private float grabCheckRadius = 0.2f;
    [SerializeField] private Vector2 grabRightOffset = new Vector2(0.5f, -0.4f);
    [SerializeField] private Vector2 grabLeftOffset = new Vector2(-0.5f, -0.4f);
    [SerializeField] private float wallSlideSpeed = 2.5f;
    [SerializeField] private Vector2 wallJumpForce = new Vector2(15f, 15f);
    
    private float dirX = 0f;
    private int airJumpCnt = 0;
    private bool isDashing = false;
    private float dashTimer = 0f;
    private float dashCoolDownTimer = 0f;
    private bool grabbingWall = false;
    private bool canMove = true;
    private static readonly int animState = Animator.StringToHash("state");

    private enum MovementState
    {
        idle = 0,
        running = 1,
        jumping = 2,
        falling = 3,
        doubleJumping = 4,
        wallSliding = 5,
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

        if (isOnWall() && !IsGrounded())
        {
            WallSlide();
        }
        
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
        
        canWallGrab = charData.canWallGrab;
        grabCheckRadius = charData.grabCheckRadius;
        grabRightOffset = charData.grabRightOffset;
        grabLeftOffset = charData.grabLeftOffset;
        wallSlideSpeed = charData.wallSlideSpeed;
        wallJumpForce = charData.wallJumpForce;
    }

    #region Input Actions
    public void ReadHorizontalMovement(InputAction.CallbackContext context)
    {
        dirX = context.ReadValue<float>();
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
    
    public void ReadJump(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            if (isOnWall() && !IsGrounded())
            {
                WallJump();
            }
            else
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
    }
    #endregion

    private void Move()
    {
        if (isOnWall() && !IsGrounded())
            return;
        
        if (!canMove)
            return;
        
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

    private void WallJump()
    {
        StopCoroutine(DisableMovement(0));
        StartCoroutine(DisableMovement(0.15f));
        
        PlayParticleEffect(jumpEffect);
        AudioManager.Instance.PlaySound(AudioType.characterJump);
        
        rb.velocity = charSprite.flipX ? new Vector2(wallJumpForce.x, wallJumpForce.y) : new Vector2(-wallJumpForce.x, wallJumpForce.y);
        
        Flip();
    }

    private void WallSlide()
    {
        if (!canMove)
            return;
        
        rb.velocity = new Vector2(rb.velocity.x, -wallSlideSpeed);
    }

    IEnumerator DisableMovement(float time)
    {
        canMove = false;
        yield return new WaitForSeconds(time);
        canMove = true;
    }

    private void UpdateAnimationState()
    {
        MovementState state = MovementState.idle;

        if (isOnWall() && !IsGrounded())
        {
            state = MovementState.wallSliding;
        }
        else
        {
            if (canMove)
            {
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
            }
        }
        
        animator.SetInteger(animState, (int)state);
    }

    private bool IsGrounded()
    {
        Bounds bounds = playerCollider.bounds;
        return Physics2D.BoxCast(bounds.center, bounds.size, 0f, Vector2.down, .1f, groundLayer);
    }

    private bool isOnWall()
    {
        if (canWallGrab)
            return isOnRightWall() || isOnLeftWall();
        else
            return false;
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

    public void Flip(bool reset = false)
    {
        if (reset)
        {
            charSprite.flipX = false;
        }
        else
        {
            charSprite.flipX = !charSprite.flipX;
        }
    }
    
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        
        Gizmos.DrawWireSphere((Vector2)transform.position + grabRightOffset, grabCheckRadius);
        Gizmos.DrawWireSphere((Vector2)transform.position + grabLeftOffset, grabCheckRadius);
    }
}