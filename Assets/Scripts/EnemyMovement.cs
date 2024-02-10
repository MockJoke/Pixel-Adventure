using UnityEngine;

public class EnemyMovement : WaypointFollower
{
    private bool isMovingRight;
    [SerializeField] private SpriteRenderer spriteRenderer;

    void Awake()
    {
        if (spriteRenderer == null)
            spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Start()
    {
        isMovingRight = transform.position.x < waypoints[currWaypointIndex].position.x;
    }

    protected override void Move()
    {
        base.Move();

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
