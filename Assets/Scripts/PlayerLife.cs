using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerLife : MonoBehaviour
{
    [SerializeField] private int LifeCount = 3;
    [SerializeField] private PlayerMovement playerMovement;
    [SerializeField] private GameObject[] hearts;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Animator animator;
    [SerializeField] private BoxCollider2D boxCollider;
    [SerializeField] private Vector3 initSpawnPos;
    [SerializeField] private Vector3 respawnOffset;
    
    [Space]
    [SerializeField] private CheckpointManager checkpointManager;
    
    private static readonly int deathAnim = Animator.StringToHash("death");

    void Awake()
    {
        if (rb == null)
            rb = GetComponent<Rigidbody2D>();
        
        if (animator == null)
            animator = GetComponent<Animator>();

        if (boxCollider == null)
            boxCollider = GetComponent<BoxCollider2D>();

        if (playerMovement == null)
            playerMovement = GetComponent<PlayerMovement>();

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

    private void DecreaseHearts()
    {
        hearts[LifeCount].SetActive(false);
    }
    
    private void CheckLifeStatus()
    {
        if (LifeCount > 0)
        {
            Respawn();
            DecreaseHearts();
        }
        else
        {
            SceneManager.LoadScene("Scenes/Game Over Screen");
        }
    }

    private void Respawn()
    {
        if (checkpointManager.hasPassedAnyCheckPoints())
        {
            Vector3 pos = checkpointManager.GetLatestCheckPoint().position;
            transform.position = new Vector3(pos.x + respawnOffset.x, pos.y + respawnOffset.y, pos.z + respawnOffset.z);
        }
        else
        {
            transform.position = initSpawnPos;
        }
        
        boxCollider.enabled = true;
        playerMovement.ResetFlipping();
    }
}
