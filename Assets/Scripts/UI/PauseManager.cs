using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseManager : MonoBehaviour
{
    [SerializeField] private Canvas pauseCanvas;
    [SerializeField] private SettingsManager settingsManager;
    [SerializeField] private Canvas controlsCanvas;
    
    void Awake()
    {
        if (pauseCanvas == null)
            pauseCanvas = GetComponent<Canvas>();
    }

    public void OpenPauseMenu()
    {
        pauseCanvas.enabled = true;
        SetTimeScale(false);
    }

    public void ClosePauseMenu(bool resetTimeScale = true)
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
        ClosePauseMenu(false);
        settingsManager.OpenMenu();
    }

    private void SetTimeScale(bool reset)
    {
        Time.timeScale = reset ? 1 : 0;
    }

    public void OpenControls()
    {
        ClosePauseMenu(false);
        controlsCanvas.enabled = true;
    }

    public void CloseControls()
    {
        controlsCanvas.enabled = false;
        OpenPauseMenu();
    }
}