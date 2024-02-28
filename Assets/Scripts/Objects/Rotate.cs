using UnityEngine;

public class Rotate : MonoBehaviour
{
    private enum direction
    {
        none,
        clockwise,
        anticlockwise
    }
    
    [SerializeField] private float speed = 2f;
    [SerializeField] private direction rotationDirection = direction.clockwise;
 
    void Update()
    {
        switch (rotationDirection)
        {
            case direction.clockwise:
                transform.Rotate(0, 0, -360 * speed * Time.deltaTime);
                break;
            case direction.anticlockwise:
                transform.Rotate(0, 0, 360 * speed * Time.deltaTime);
                break;
            case direction.none:
                break;
            default:
                transform.Rotate(0, 0, -360 * speed * Time.deltaTime);
                break;
        }
    }
}
