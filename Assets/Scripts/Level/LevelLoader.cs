using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Button))]
public class LevelLoader : MonoBehaviour
{
    [SerializeField] private Button levelBtn;
    [SerializeField] private LevelName levelName;

    void Awake()
    {
        if (levelBtn == null)
            levelBtn = GetComponent<Button>();
        
        levelBtn.onClick.AddListener(onLevelBtnClick); 
    }

    void Start()
    {
        setLevelStatusOnButton();
    }

    private void onLevelBtnClick()
    {
        LevelStatus levelStatus = LevelManager.Instance.GetLevelStatus(levelName);
        
        switch (levelStatus)
        {
            case LevelStatus.Locked:
                Debug.Log("Can't play this level till you unlock it!");
                break;

            case LevelStatus.Unlocked:
                SceneManager.LoadScene(levelName.ToString());
                break;

            case LevelStatus.Completed:
                SceneManager.LoadScene(levelName.ToString());
                break;
        }
    }

    private void setLevelStatusOnButton()
    {
        levelBtn.interactable = LevelManager.Instance.GetLevelStatus(levelName) != LevelStatus.Locked;
    }

    #region Debug Methods
    // After unlocking all levels from LevelManager, need to refresh individual level loader buttons
    // to set their interactivity again based on LevelStatus, because their status is getting set on start()
    // & during its UI life cycle we are never enabling-disabling it so can't use of OnEnable()
    [ContextMenu("Refresh Level Status")]
    private void RefreshLevelStatus()
    {
        setLevelStatusOnButton();
    }
    #endregion
}