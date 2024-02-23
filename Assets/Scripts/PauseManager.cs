using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseManager : MonoBehaviour
{
    [SerializeField] private Canvas pauseCanvas;
    [SerializeField] private SettingsManager settingsManager;
    
    void Awake()
    {
        if (pauseCanvas == null)
            pauseCanvas = GetComponent<Canvas>();
    }

    public void OpenMenu()
    {
        pauseCanvas.enabled = true;
        SetTimeScale(false);
    }

    public void CloseMenu(bool resetTimeScale = true)
    {
        pauseCanvas.enabled = false;
        
        if (resetTimeScale)
            SetTimeScale(true);
    }

    public void HomeMenu()
    {
        SceneManager.LoadScene("Scenes/Start Screen");
        SetTimeScale(true);
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        SetTimeScale(true);
    }

    public void Settings()
    {
        CloseMenu(false);
        settingsManager.OpenMenu();
    }

    private void SetTimeScale(bool reset)
    {
        Time.timeScale = reset ? 1 : 0;
    }
}