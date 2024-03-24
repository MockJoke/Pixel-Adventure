using UnityEngine;

public class EnemyDamage : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private Animator animator;
    [SerializeField] private Collider2D enemyCollider;
    private static readonly int dieAnim = Animator.StringToHash("Die");

    void Awake()
    {
        if (animator == null)
            animator = GetComponent<Animator>();

        if (enemyCollider == null)
            enemyCollider = GetComponent<Collider2D>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (DotTest(collision.transform, transform, Vector2.down)) 
            {
                CameraShaker.Instance.ShakeCamera(5f, 0.25f);
                Die();
            } 
            else 
            {
                PlayerLife player = collision.gameObject.GetComponent<PlayerLife>();
                player.Die();
            }
        }
    }
    
    private bool DotTest(Transform main, Transform other, Vector2 testDirection)
    {
        Vector2 direction = other.position - main.position;
        return Vector2.Dot(direction.normalized, testDirection) > 0.25f;
    }

    public void Die()
    {
        AudioManager.Instance.PlaySound(AudioType.enemyHit);
        animator.SetTrigger(dieAnim);
        enemyCollider.enabled = false;
        Destroy(gameObject, 1.25f);
    }
}