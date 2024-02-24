using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverMenu : MonoBehaviour
{
    public void StartAgain()
    {
        SceneManager.LoadScene(LevelManager.Instance.GetLatestUnlockedLevelNo() + 1);
    }
}
