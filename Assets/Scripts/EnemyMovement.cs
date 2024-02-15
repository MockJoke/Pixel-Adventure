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
        
        if (Vector2.Distance(waypoints[currWaypointIndex].position, transform.position) < closingDistance)
        {
            if (currWaypointIndex == 0 || currWaypointIndex == waypoints.Length - 1)
                Flip();
        }
        
        animator.SetFloat(speedAnim, speed);
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
