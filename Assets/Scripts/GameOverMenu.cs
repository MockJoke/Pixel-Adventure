using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverMenu : MonoBehaviour
{
    [SerializeField] private Canvas gameOverCanvas;
    [SerializeField] private PlayerLife playerLife;

    private void Awake()
    {
        if (gameOverCanvas == null)
            gameOverCanvas = GetComponent<Canvas>();
    }

    public void OpenMenu()
    {
        gameOverCanvas.enabled = true;
        SetTimeScale(false);
    }
    
    public void CloseMenu()
    {
        gameOverCanvas.enabled = false;
        SetTimeScale(true);
    }
    
    private void SetTimeScale(bool reset)
    {
        Time.timeScale = reset ? 1 : 0;
    }
    
    public void Home()
    {
        CloseMenu();
        SceneManager.LoadScene("Scenes/Start Screen");
    }
    
    public void StartAgain()
    {
        CloseMenu();
        playerLife.GiveLife();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
