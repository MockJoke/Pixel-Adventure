using UnityEditor;
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
    [SerializeField] private Quaternion defaultRot = Quaternion.Euler(0, 0, 0);
    
    private bool movingClockwise = true;
    private float direction = -1;    // -1 for clockwise, 1 for counter-clockwise
    
    private float angle;
    private float radius;
    
    private Vector3 initPos;
    
    void Awake()
    {
        if (rb2d == null)
            rb2d = GetComponent<Rigidbody2D>();
    }

    void Start()
    {
        initPos = transform.position;
        angle = GetObjectRotation();
        
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
    
    private float GetObjectRotation()
    {
        if(transform.eulerAngles.z > 180)
        {
            return transform.eulerAngles.z - 360;
        }
        else
        {
            return transform.eulerAngles.z;
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
    
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;

        radius = Vector3.Distance(plank.position, ball.position);

        Vector3 center = EditorApplication.isPlaying ? initPos : transform.position;
        
        if (closedLoop)
        {
            // Draw a full circle
            DrawCircle(center, radius);
        }
        else
        {
            // Draw arc based on angle limits
            float arcStart = leftAngle;
            float arcEnd = rightAngle;

            // Draw arc from leftAngle to rightAngle
            DrawArc(center, defaultRot,  radius, arcStart, arcEnd, 0.1f);
        }
    }

    private void DrawCircle(Vector3 center, float r)
    {
        float theta = 0;
        float x = r * Mathf.Cos(theta);
        float y = r * Mathf.Sin(theta);
        Vector3 previous = center + new Vector3(x, y, 0);

        for (int i = 1; i <= 360; i++)
        {
            theta = Mathf.Deg2Rad * i;
            x = r * Mathf.Cos(theta);
            y = r * Mathf.Sin(theta);
            Vector3 next = center + new Vector3(x, y, 0);
            Gizmos.DrawLine(previous, next);
            previous = next;
        }
    }

    private void DrawArc(Vector3 center, Quaternion rot, float r, float startAngle, float endAngle, float step)
    {
        Quaternion pendulumRotation = Quaternion.Euler(0, 0, rot.eulerAngles.z);

        Vector3 startPoint = Quaternion.Euler(0, 0, startAngle) * Vector3.down * r;
        startPoint = center + pendulumRotation * startPoint;

        Vector3 previous = startPoint;

        for (float theta = startAngle + step; theta <= endAngle; theta += step)
        {
            Vector3 nextPoint = Quaternion.Euler(0, 0, theta) * Vector3.down * r;
            nextPoint = center + pendulumRotation * nextPoint;

            Gizmos.DrawLine(previous, nextPoint);
            previous = nextPoint;
        }
    }
}
