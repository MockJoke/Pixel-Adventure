using UnityEngine;

public class Finish : MonoBehaviour
{
    [SerializeField] private GameObject confetti;
    private bool levelCompleted = false;

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Player" && !levelCompleted)
        {
            AudioManager.Instance.PlaySound(AudioType.levelFinish);
            confetti.SetActive(true);
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
