using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverMenu : MonoBehaviour
{
    [SerializeField] private Canvas gameOverCanvas;
    [SerializeField] private GameObject mainPanel;
    [SerializeField] private GameObject startLevelAgainBtn;

    private void Awake()
    {
        if (gameOverCanvas == null)
            gameOverCanvas = GetComponent<Canvas>();
    }

    public void OpenGameOverMenu()
    {
        gameOverCanvas.enabled = true;
        SetTimeScale(false);

        SetStartAgainBtn();
    }
    
    public void CloseGameOverMenu()
    {
        gameOverCanvas.enabled = false;
        SetTimeScale(true);
    }

    private void SetStartAgainBtn()
    {
        startLevelAgainBtn.SetActive(PlayerPrefs.GetInt("LifeCount", 0) > 0);
    }
    
    private void SetTimeScale(bool reset)
    {
        Time.timeScale = reset ? 1 : 0;
    }
    
    public void Home()
    {
        CloseGameOverMenu();
        SceneManager.LoadScene("Scenes/Start Screen");
    }
    
    public void StartAgain()
    {
        CloseGameOverMenu();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void OnExtraLivesMenuOpen()
    {
        mainPanel.SetActive(false);
    }
    
    public void OnExtraLivesMenuClose()
    {
        mainPanel.SetActive(true);
        
        SetStartAgainBtn();
    }
}
