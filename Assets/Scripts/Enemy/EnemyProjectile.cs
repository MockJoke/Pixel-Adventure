using UnityEngine;

public class EnemyProjectile : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private Animator animator;
    [SerializeField] private BoxCollider2D coll;
    
    [Header("Movement")]
    [SerializeField] private float speed;
    [SerializeField] private float resetTime;
    private float lifetime;

    private bool hit;
    private static readonly int explodeAnim = Animator.StringToHash("Explode");

    void Awake()
    {
        if (animator == null)
            animator = GetComponent<Animator>();
        
        if (coll == null)
            coll = GetComponent<BoxCollider2D>();
    }

    public void ActivateProjectile()
    {
        hit = false;
        lifetime = 0;
        gameObject.SetActive(true);
        coll.enabled = true;
    }
    
    private void Update()
    {
        if (hit) 
            return;
        
        float movementSpeed = speed * Time.deltaTime;
        transform.Translate(movementSpeed, 0, 0);

        lifetime += Time.deltaTime;
        
        if (lifetime > resetTime)
            gameObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log(collision.gameObject);
        hit = true;
        
        if (collision.CompareTag("Player"))
            collision.GetComponent<PlayerLife>().Die();
        
        coll.enabled = false;

        if (animator != null)
            animator.SetTrigger(explodeAnim); //When the object is a fireball explode it
    }
    
    private void Deactivate()
    {
        gameObject.SetActive(false);
    }
}
