using UnityEngine;
using UnityEngine.SceneManagement;

public class StartMenu : MonoBehaviour
{
    [SerializeField] private Canvas HomeCanvas;
    [SerializeField] private Canvas LevelsCanvas;
    [SerializeField] private Canvas SettingsCanvas;
    
    public void StartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void Levels()
    {
        HomeCanvas.enabled = false;
        LevelsCanvas.enabled = true;
    }
    
    public void Settings()
    {
        HomeCanvas.enabled = false;
        SettingsCanvas.enabled = true;
    }
}
