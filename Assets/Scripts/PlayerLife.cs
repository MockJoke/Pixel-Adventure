using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerLife : MonoBehaviour
{
    [SerializeField] private int LifeCount = 3;
    [SerializeField] private GameObject[] hearts;
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

        LifeCount = PlayerPrefs.GetInt("LifeCount", 3);
        
        SetHearts();
    }

    public void Die()
    {
        boxCollider.enabled = false;
        CameraShaker.Instance.ShakeCamera(5f, 0.25f);
        AudioManager.Instance.PlaySound(AudioType.characterDeath);
        animator.SetTrigger(deathAnim);

        LoseLife();
    }

    private void LoseLife()
    {
        LifeCount--;
        PlayerPrefs.SetInt("LifeCount", LifeCount);
    }

    private void SetHearts()
    {
        for (int i = 0; i < LifeCount; i++)
        {
            hearts[i].SetActive(true);
        }
    }
    
    private void CheckLifeStatus()
    {
        if (LifeCount > 0)
        {
            RestartLevel();
        }
        else
        {
            SceneManager.LoadScene("Scenes/Game Over Screen");
        }
    }
    
    private void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
