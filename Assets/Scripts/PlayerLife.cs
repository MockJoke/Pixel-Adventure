using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerLife : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Animator animator;
    private static readonly int deathAnim = Animator.StringToHash("death");

    void Awake()
    {
        if (rb == null)
            rb = GetComponent<Rigidbody2D>();
        
        if (animator == null)
            animator = GetComponent<Animator>();
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
        AudioManager.instance.PlaySound(AudioType.characterDeath);
        rb.bodyType = RigidbodyType2D.Static;
        animator.SetTrigger(deathAnim);
    }

    private void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
