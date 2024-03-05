using UnityEngine;

public class Pendulum : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private Rigidbody2D rb2d;
    
    [Header("Movement Parameters")]
    [SerializeField] private float moveSpeed;
    [SerializeField] private float leftAngle;
    [SerializeField] private float rightAngle;

    private bool movingClockwise;
    
    void Start()
    {
        if (rb2d == null)
            rb2d = GetComponent<Rigidbody2D>();
        
        movingClockwise = true;
    }

    void Update()
    {
        Move();
    }

    private void ChangeMoveDir()
    {
        if (transform.rotation.z > rightAngle)
        {
            movingClockwise = false;
        }
        else if (transform.rotation.z < leftAngle)
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
