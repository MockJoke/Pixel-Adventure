using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSwordAttack : MonoBehaviour
{
    [SerializeField] private Animator animator;
    
    private static readonly int attackAnim = Animator.StringToHash("attack");

    private void Awake()
    {
        if (animator == null)
            animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            Attack();
        }
    }

    private void Attack()
    {
        animator.SetTrigger(attackAnim);
    }
}
