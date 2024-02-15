using UnityEngine;

public class EnemyMovement : WaypointFollower
{
    private bool isMovingRight;
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private Animator animator;
    private static readonly int speedAnim = Animator.StringToHash("Speed");

    void Awake()
    {
        if (spriteRenderer == null)
            spriteRenderer = GetComponent<SpriteRenderer>();

        if (animator == null)
            animator = GetComponent<Animator>();
    }

    void Start()
    {
        isMovingRight = transform.position.x < waypoints[currWaypointIndex].position.x;
    }

    protected override void Move()
    {
        base.Move();
        
        animator.SetFloat(speedAnim, speed);
        
        if (Vector2.Distance(waypoints[currWaypointIndex].position, transform.position) < .1f)
        {
            if (currWaypointIndex == 0 || currWaypointIndex == waypoints.Length - 1)
                Flip();
        }
    }
    
    private void Flip()
    {
        if (isMovingRight)
        {
            spriteRenderer.flipX = false;
            isMovingRight = false;
        }
        else
        {
            spriteRenderer.flipX = true;
            isMovingRight = true;
        }
    }
}
