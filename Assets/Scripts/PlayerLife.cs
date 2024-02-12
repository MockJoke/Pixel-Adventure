using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerLife : MonoBehaviour
{
    // [SerializeField] private int lifeCount = 3;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Animator animator;
    [SerializeField] private BoxCollider2D boxCollider;
    private static readonly int deathAnim = Animator.StringToHash("death");

    void Awake()
    {
        if (rb == null)
            rb = GetComponent<Rigidbody2D>();
        
        if (animator == null)
            animator = GetComponent<Animator>();

        if (boxCollider == null)
            boxCollider = GetComponent<BoxCollider2D>();
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Trap"))
        {
            Die();
        }
    }

    private void Die()
    {
        CameraShaker.instance.ShakeCamera(5f, 0.25f);
        AudioManager.instance.PlaySound(AudioType.characterDeath);
        animator.SetTrigger(deathAnim);
        boxCollider.enabled = false;
    }

    private void CheckLifeStatus()
    {
        RestartLevel();
        
        // if (lifeCount >= 0)
        // {
        //     RestartLevel();
        // }
        // else
        // {
        //     SceneManager.LoadScene("Scenes/End Screen");
        // }
    }
    
    private void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
