using System;
using UnityEngine;

public class Fan : MonoBehaviour
{
    private enum Direction
    {
        up,
        down,
        left,
        right
    }
    
    [SerializeField] private float initForce = 10f;
    [SerializeField] private float maxForce = 20f;
    [SerializeField] private float forceIncreaseRate = 5f;
    [SerializeField] private Direction facingDirection = Direction.up;

    private Rigidbody2D playerRb;
    private float currForce = 0f;
    
    void FixedUpdate()
    {
        if (playerRb != null)
        {
            Vector3 forceDir = GetForceDir();
            
            playerRb.AddForce(forceDir.normalized * currForce, ForceMode2D.Force);

            if (currForce < maxForce)
            {
                currForce += forceIncreaseRate * Time.fixedDeltaTime;
            }
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            playerRb = collision.gameObject.GetComponent<Rigidbody2D>();
            currForce = initForce;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            playerRb = null;
            currForce = initForce;
        }
    }

    private Vector3 GetForceDir()
    {
        switch (facingDirection)
        {
            case Direction.up:
                return transform.up;
            case Direction.down:
                return -transform.up;
            case Direction.left:
                return -transform.right;
            case Direction.right:
                return transform.right;
            default:
                return Vector3.zero;
        }
    }
}
