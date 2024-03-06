using UnityEngine;

public class Rotate : MonoBehaviour
{
    [SerializeField] private float speed = 2f;
    [SerializeField] private RotationDirection rotationDirection = RotationDirection.clockwise;
 
    void Update()
    {
        switch (rotationDirection)
        {
            case RotationDirection.clockwise:
                transform.Rotate(0, 0, -360 * speed * Time.deltaTime);
                break;
            case RotationDirection.anticlockwise:
                transform.Rotate(0, 0, 360 * speed * Time.deltaTime);
                break;
            case RotationDirection.none:
                break;
            default:
                transform.Rotate(0, 0, -360 * speed * Time.deltaTime);
                break;
        }
    }
}
