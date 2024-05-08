using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class StartMenu : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI startBtnText;
    [SerializeField] private Canvas HomeCanvas;
    [SerializeField] private Canvas LevelsCanvas;
    [SerializeField] private CharacterSelectionMenu characterSelectionMenu;
    [SerializeField] private SettingsManager settingsManager;
    [SerializeField] private Canvas ControlsCanvas;
    [SerializeField] private InsufficientLivesMenu insufficientLivesMenu;
    
    [Tooltip("The first object to be selected in Start-Menu during UI navigation for controls other than mouse")]
    [SerializeField] private GameObject firstSelectedStartMenuObj;
    
    [Tooltip("The first object to be selected in Level-Menu during UI navigation for controls other than mouse")]
    [SerializeField] private GameObject firstSelectedLevelMenuObj;

    void Awake()
    {
        EventSystem.current.SetSelectedGameObject(firstSelectedStartMenuObj);
    }

    void Start()
    {
        startBtnText.text = LevelManager.Instance.GetLatestUnlockedLevelNo() == 0 ? "START" : "CONTINUE";
    }

    public void StartGame()
    {
        if(PlayerPrefs.GetInt("LifeCount", 3) > 0)
        {
            LevelManager.Instance.GoToLatestUnlockedLevel();
        }
        else
        {
            OpenInsufficientLivesMenu();
        }
    }
    
    public void OpenHomeMenu()
    {
        EventSystem.current.SetSelectedGameObject(firstSelectedStartMenuObj);
        HomeCanvas.enabled = true;
    }
    
    public void CloseHomeMenu()
    {
        HomeCanvas.enabled = false;
    }
    
    public void OpenLevelsMenu()
    {
        if (PlayerPrefs.GetInt("LifeCount", 3) > 0)
        {
            CloseHomeMenu();
            LevelsCanvas.enabled = true;
            EventSystem.current.SetSelectedGameObject(firstSelectedLevelMenuObj);
        }
        else
        {
            OpenInsufficientLivesMenu();
        }
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

    public void OpenInsufficientLivesMenu()
    {
        // CloseHomeMenu();
        insufficientLivesMenu.OpenMenu();
    }
}