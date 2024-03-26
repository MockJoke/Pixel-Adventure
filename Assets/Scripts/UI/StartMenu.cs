using TMPro;
using UnityEngine;

public class StartMenu : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI startBtnText;
    [SerializeField] private Canvas HomeCanvas;
    [SerializeField] private Canvas LevelsCanvas;
    [SerializeField] private CharacterSelectionMenu characterSelectionMenu;
    [SerializeField] private SettingsManager settingsManager;
    [SerializeField] private Canvas ControlsCanvas;
    
    void Awake()
    {
        startBtnText.text = LevelManager.Instance.GetLatestUnlockedLevelNo() == 0 ? "START" : "CONTINUE";
    }

    public void StartGame()
    {
        LevelManager.Instance.GoToLatestUnlockedLevel();
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

    public void OpenCharSelectionMenu()
    {
        CloseHomeMenu();
        characterSelectionMenu.OpenMenu();
    }

    public void OpenControlsMenu()
    {
        CloseHomeMenu();
        ControlsCanvas.enabled = true;
    }

    public void CloseControlsMenu()
    {
        ControlsCanvas.enabled = false;
        OpenHomeMenu();
    }
}
