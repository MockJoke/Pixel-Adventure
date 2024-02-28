using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private Animator animator;
    
    [Header ("Patrol Points")]
    [SerializeField] private Transform leftEdge;
    [SerializeField] private Transform rightEdge;
    
    [Header("Movement parameters")]
    [SerializeField] private float speed = 2f;
    [SerializeField] private int defaultSpriteFacing = -1;
    [SerializeField] private bool isRangedEnemy = false;
    [SerializeField] private Transform bulletsHolder;
    [SerializeField] private float idleDuration = 1f;
    
    private Vector3 initScale;
    private bool movingLeft = true;
    private float idleTimer;
    
    private static readonly int movingAnim = Animator.StringToHash("Moving");

    void Awake()
    {
        if (spriteRenderer == null)
            spriteRenderer = GetComponent<SpriteRenderer>();

        if (animator == null)
            animator = GetComponent<Animator>();
        
        initScale = transform.localScale;
    }

    void Start()
    {
        movingLeft = defaultSpriteFacing == -1;
    }

    private void OnDisable()
    {
        animator.SetBool(movingAnim, false);
    }

    void Update()
    {
        if (movingLeft)
        {
            if (transform.position.x >= leftEdge.position.x)
                MoveInDirection(-1);
            else
                DirectionChange();
        }
        else
        {
            if (transform.position.x <= rightEdge.position.x)
                MoveInDirection(1);
            else
                DirectionChange();
        }
    }

    private void DirectionChange()
    {
        animator.SetBool(movingAnim, false);
        idleTimer += Time.deltaTime;

        if(idleTimer > idleDuration)
            movingLeft = !movingLeft;
    }

    private void MoveInDirection(int _direction)
    {
        idleTimer = 0;
        animator.SetBool(movingAnim, true);

        //Make enemy face direction
        transform.localScale = new Vector3(Mathf.Abs(initScale.x) * _direction * defaultSpriteFacing,
            initScale.y, initScale.z);
        
        // Change the scale of bullets holder for updating the bullet direction
        if (isRangedEnemy)
            bulletsHolder.localScale = -transform.localScale;

        //Move in that direction
        transform.position = new Vector3(transform.position.x + Time.deltaTime * _direction * speed,
            transform.position.y, transform.position.z);
    }
}
