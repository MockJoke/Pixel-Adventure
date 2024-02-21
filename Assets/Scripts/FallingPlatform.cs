using UnityEngine;

public class FallingPlatform : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Animator animator;
    [SerializeField] private SpringJoint2D springJoint;
    
    [Header("Falling Parameters")]
    [SerializeField] private float fallDelay = 2.5f;
    [SerializeField] private float destroyDelay = 3f;
    
    private static readonly int fallingAnim = Animator.StringToHash("Falling");

    void Awake()
    {
        if (rb == null)
            rb = GetComponent<Rigidbody2D>();

        if (animator == null)
            animator = GetComponent<Animator>();

        if (springJoint == null)
            springJoint = GetComponent<SpringJoint2D>();
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            springJoint.enabled = true;
            rb.bodyType = RigidbodyType2D.Dynamic;
            Invoke(nameof(Fall), fallDelay);
        }
    }

    private void Fall()
    {
        springJoint.enabled = false;
        animator.SetTrigger(fallingAnim);
        
        Destroy(gameObject, destroyDelay);
    }
}
