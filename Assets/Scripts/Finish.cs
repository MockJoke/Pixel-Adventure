using UnityEngine;
using UnityEngine.SceneManagement;

public class Finish : MonoBehaviour
{
    private bool levelCompleted = false;

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Player" && !levelCompleted)
        {
            AudioManager.Instance.PlaySound(AudioType.levelFinish);
            levelCompleted = true;
            Invoke(nameof(CompleteLevel), 2f);
        }
    }

    private void CompleteLevel()
    {
        LevelManager.Instance.MarkLevelComplete();
        LevelManager.Instance.GoToNextLevel();
        // SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
