using System;
using UnityEngine;

public class PlayerSwordAttack : MonoBehaviour
{
    [SerializeField] private Animator animator;

    private bool attacking = false;
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

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!attacking) return;
        
        if (collision.gameObject.CompareTag("Enemy"))
        {
            CameraShaker.Instance.ShakeCamera(3f, 0.35f);
            collision.gameObject.GetComponent<EnemyDamage>().Die();
        }
    }
}
