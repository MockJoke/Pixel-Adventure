using UnityEngine;

public class PlayerLife : MonoBehaviour
{
    [SerializeField] private int LifeCount = 3;
    [SerializeField] public int maxLifeBound = 100;
    [SerializeField] private GameObject[] hearts;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Animator animator;
    [SerializeField] private BoxCollider2D boxCollider;
    [SerializeField] private Vector3 initSpawnPos;
    [SerializeField] private Vector3 respawnOffset;
    
    [Header("Manager Refs")]
    [SerializeField] private PlayerMovement playerMovement;
    [SerializeField] private GameOverMenu gameOverMenu;
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
        if (LifeCount > 0)
        {
            LifeCount--;
        }
        
        PlayerPrefs.SetInt("LifeCount", LifeCount);
    }

    public void GiveLife(int count = 1)
    {
        if (LifeCount < maxLifeBound)
        {
            LifeCount += count;
        }
        
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
        DecreaseHearts();
        
        if (LifeCount > 0)
        {
            Respawn();
        }
        else
        {
            gameOverMenu.OpenMenu();
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
