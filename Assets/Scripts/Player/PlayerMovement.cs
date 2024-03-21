using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private BoxCollider2D playerCollider;
    [SerializeField] private SpriteRenderer charSprite;
    [SerializeField] private Animator animator;
    [SerializeField] private ParticleSystem dust;
    
    [Header("Movement")]
    [SerializeField] private float moveSpeed = 7f;
    [SerializeField] private float jumpForce = 14f;
    [SerializeField] private float doubleJumpForce = 10f;
    [SerializeField] private int maxAirJumpCnt = 1;
    [SerializeField] private LayerMask groundLayer;

    private int airJumpCnt = 0;
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

        if (charSprite == null)
            charSprite = GetComponent<SpriteRenderer>();

        if (animator == null)
            animator = GetComponent<Animator>();

        if (dust == null)
            dust = GetComponentInChildren<ParticleSystem>();
    }

    void Start()
    {
        airJumpCnt = maxAirJumpCnt;
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
            airJumpCnt = 0;
        }

        if (Input.GetButton("Jump"))
        {
            if (IsGrounded())
            {
                Jump(jumpForce);
            }
            else if (Input.GetButtonDown("Jump") && airJumpCnt < maxAirJumpCnt)
            {
                Jump(doubleJumpForce);
                airJumpCnt++;
            }
        }

        UpdateAnimationState();
    }

    public void SetData(CharacterDataSO.CharacterData charData)
    {
        charSprite.sprite = charData.charSprite;
        animator.runtimeAnimatorController = charData.animatorController;

        moveSpeed = charData.moveSpeed;
        jumpForce = charData.jumpForce;
        doubleJumpForce = charData.doubleJumpForce;
        maxAirJumpCnt = charData.maxAirJumpCnt;
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
            charSprite.flipX = false;
        }
        else if (dirX < 0f) 
        {
            CreateDust();
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
    
    private void CreateDust() 
    {
        dust.Play();
    }

    public void ResetFlipping()
    {
        charSprite.flipX = false;
    }
}