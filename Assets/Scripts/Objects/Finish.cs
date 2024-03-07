using UnityEngine;

public class Finish : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private Animator animator;
    [SerializeField] private GameObject confetti;
    
    [Header("Manager Refs")]
    [SerializeField] private CheckpointManager checkpointManager;
    
    private bool levelCompleted = false;
    private static readonly int onCompleteAnim = Animator.StringToHash("OnComplete");

    void Awake()
    {
        if (animator == null)
            animator = GetComponent<Animator>();

        if (checkpointManager == null)
            checkpointManager = FindObjectOfType<CheckpointManager>();
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Player" && !levelCompleted)
        {
            animator.SetTrigger(onCompleteAnim);
            AudioManager.Instance.PlaySound(AudioType.levelFinish);
            confetti.SetActive(true);
            
            checkpointManager.ClearCheckpoints();
            
            levelCompleted = true;
            Invoke(nameof(CompleteLevel), 2.15f);
        }
    }

    private void CompleteLevel()
    {
        LevelManager.Instance.MarkLevelComplete();
        LevelManager.Instance.GoToNextLevel();
    }
}
