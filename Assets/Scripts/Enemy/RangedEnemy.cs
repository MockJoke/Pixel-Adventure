using UnityEngine;

public class RangedEnemy : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private Animator animator;
    [SerializeField] private EnemyMovement enemyMovement;
    
    [Header("Attack Parameters")]
    [SerializeField] private float attackCooldown;
    [SerializeField] private float range;
    [SerializeField] private int damage;
    [SerializeField] private Transform bulletPoint;
    [SerializeField] private GameObject[] bullets;

    [Header("Collider Parameters")]
    [SerializeField] private float colliderDistance;
    [SerializeField] private BoxCollider2D boxCollider;
    [SerializeField] private LayerMask playerLayer;
    
    private float cooldownTimer = Mathf.Infinity;
    private static readonly int rangedAttackAnim = Animator.StringToHash("RangedAttack");

    void Awake()
    {
        if (animator == null)
            animator = GetComponent<Animator>();
        
        if (enemyMovement == null)
            enemyMovement = GetComponentInParent<EnemyMovement>();
    }

    void Update()
    {
        cooldownTimer += Time.deltaTime;

        //Attack only when player in sight?
        if (PlayerInSight())
        {
            if (cooldownTimer >= attackCooldown)
            {
                cooldownTimer = 0;
                animator.SetTrigger(rangedAttackAnim);
            }
        }

        if (enemyMovement != null)
            enemyMovement.enabled = !PlayerInSight();
    }

    private void RangedAttack()
    {
        // AudioManager.Instance.PlaySound(fireballSound);
        cooldownTimer = 0;
        bullets[FindBullet()].transform.position = bulletPoint.position;
        bullets[FindBullet()].GetComponent<EnemyProjectile>().ActivateProjectile();
    }
    
    private int FindBullet()
    {
        for (int i = 0; i < bullets.Length; i++)
        {
            if (!bullets[i].activeInHierarchy)
                return i;
        }
        
        return 0;
    }

    private bool PlayerInSight()
    {
        RaycastHit2D hit =
            Physics2D.BoxCast(boxCollider.bounds.center + transform.right * range * transform.localScale.x * colliderDistance,
            new Vector3(boxCollider.bounds.size.x * range, boxCollider.bounds.size.y, boxCollider.bounds.size.z),
            0, Vector2.left, 0, playerLayer);

        return hit.collider != null;
    }
    
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(boxCollider.bounds.center + transform.right * range * transform.localScale.x * colliderDistance,
            new Vector3(boxCollider.bounds.size.x * range, boxCollider.bounds.size.y, boxCollider.bounds.size.z));
    }
}
