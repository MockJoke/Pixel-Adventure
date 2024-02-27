using UnityEngine;
using UnityEngine.SceneManagement;

public class EndScreenMenu : MonoBehaviour
{
    [SerializeField] private Canvas confirmationCanvas;

    public void OpenConfirmationPanel()
    {
        confirmationCanvas.enabled = true;
    }

    public void OnConfirm()
    {
        LevelManager.Instance.ResetLevelProgress();
        SceneManager.LoadScene("Scenes/Start Screen");
        
        PlayerPrefs.SetInt("LifeCount", 3);
        PlayerPrefs.SetInt("Foods", 0);
    }

    public void OnReject()
    {
        confirmationCanvas.enabled = false;
    }
}
