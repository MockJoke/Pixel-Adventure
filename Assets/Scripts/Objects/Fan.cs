using System;
using UnityEngine;

public class Fan : MonoBehaviour
{
    [SerializeField] private float initForce = 10f;
    [SerializeField] private float maxForce = 20f;
    [SerializeField] private float forceIncreaseRate = 5f;
    [SerializeField] private MovementDirection facingDirection = MovementDirection.up;

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
            case MovementDirection.up:
                return transform.up;
            case MovementDirection.down:
                return -transform.up;
            case MovementDirection.left:
                return -transform.right;
            case MovementDirection.right:
                return transform.right;
            default:
                return Vector3.zero;
        }
    }
}
