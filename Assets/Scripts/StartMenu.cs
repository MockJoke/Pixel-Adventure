using UnityEngine;
using UnityEngine.SceneManagement;

public class StartMenu : MonoBehaviour
{
    [SerializeField] private Canvas HomeCanvas;
    [SerializeField] private Canvas LevelsCanvas;
    [SerializeField] private SettingsManager settingsManager;
    
    public void StartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    
    public void OpenHomeMenu()
    {
        HomeCanvas.enabled = true;
    }
    
    public void CloseHomeMenu()
    {
        HomeCanvas.enabled = false;
    }
    
    public void OpenLevelsMenu()
    {
        CloseHomeMenu();
        LevelsCanvas.enabled = true;
    }

    public void CloseLevelsMenu()
    {
        LevelsCanvas.enabled = false;
        OpenHomeMenu();
    }
    
    public void OpenSettingsMenu()
    {
        CloseHomeMenu();
        settingsManager.OpenMenu();
    }
}
