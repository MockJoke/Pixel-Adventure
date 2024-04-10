using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class PauseManager : MonoBehaviour
{
    [SerializeField] private Canvas pauseCanvas;
    [SerializeField] private SettingsManager settingsManager;
    [SerializeField] private Canvas controlsCanvas;
    [SerializeField] private PlayerInput playerInput;
    // private PlayerInputActions playerInputActions;
    
    void Awake()
    {
        if (pauseCanvas == null)
            pauseCanvas = GetComponent<Canvas>();
        
        if (playerInput == null)
            playerInput = FindObjectOfType<PlayerInput>();
    }

    public void TogglePauseMenu()
    {
        // playerInputActions = inputActions;
        
        if (pauseCanvas.enabled)
        {
            ClosePauseMenu();
        }
        else
        {
            OpenPauseMenu();
        }
    }

    public void OpenPauseMenu()
    {
        pauseCanvas.enabled = true;
        SetTimeScale(false);
        playerInput.SwitchCurrentActionMap("UI");
        // playerInputActions.Player.Disable();
        // playerInputActions.UI.Enable();
    }

    public void ClosePauseMenu(bool resetTimeScale = true)
    {
        pauseCanvas.enabled = false;
        
        if (resetTimeScale)
            SetTimeScale(true);
        
        playerInput.SwitchCurrentActionMap("Player");
        // playerInputActions.Player.Enable();
        // playerInputActions.UI.Disable();
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