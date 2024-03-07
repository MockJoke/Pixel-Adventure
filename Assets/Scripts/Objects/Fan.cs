using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fan : MonoBehaviour
{
    // [SerializeField] private Animator animator;
    [SerializeField] private float force = 20f;
    private static readonly int activatedAnim = Animator.StringToHash("Activated");

    void Awake()
    {
        // if (animator == null)
        //     animator = GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            // animator.SetTrigger(activatedAnim);
            
            collision.gameObject.GetComponent<Rigidbody2D>().AddForce(Vector2.up * force, ForceMode2D.Impulse);
        }
    }
}
