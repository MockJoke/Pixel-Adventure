using UnityEngine;

public class Pendulum : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private Rigidbody2D rb2d;
    [SerializeField] private Transform plank;
    [SerializeField] private Transform ball;
    
    [Header("Movement Parameters")]
    [SerializeField] private bool closedLoop = true;
    [SerializeField] private RotationDirection rotationDirection = RotationDirection.clockwise;
    [SerializeField] private float moveSpeed;
    [SerializeField] private float leftAngle;
    [SerializeField] private float rightAngle;
    
    private bool movingClockwise = true;
    private float angle;
    private float direction = -1;    // -1 for clockwise, 1 for counter-clockwise
    
    void Start()
    {
        if (rb2d == null)
            rb2d = GetComponent<Rigidbody2D>();

        switch (rotationDirection)
        {
            case RotationDirection.clockwise:
                movingClockwise = true;
                direction = -1;
                break;
            case RotationDirection.anticlockwise:
                movingClockwise = false;
                direction = 1;
                break;
            default:
                movingClockwise = true;
                direction = -1;
                break;
        }
    }

    void Update()
    {
        Move();
    }

    private void ChangeDir()
    {
        movingClockwise = !movingClockwise;
        
        direction *= -1;
    }

    private void Move()
    {
        angle += moveSpeed * direction * Time.deltaTime;

        if (!closedLoop)
        {
            if (movingClockwise)
            {
                if (angle < leftAngle)
                {
                    angle = leftAngle;
                    ChangeDir();
                }
            }
            else
            {
                if (angle > rightAngle)
                {
                    angle = rightAngle;
                    ChangeDir();
                }
            }
        }

        rb2d.MoveRotation(angle);
    }
        {
            movingClockwise = true;
        }
    }

    private void Move()
    {
        ChangeMoveDir();

        if (movingClockwise)
        {
            rb2d.angularVelocity = moveSpeed;
        }
        else
        {
            rb2d.angularVelocity = -1 * moveSpeed;
        }
    }
}
