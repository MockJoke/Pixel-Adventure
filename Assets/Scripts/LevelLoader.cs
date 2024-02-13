using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Button))]
public class LevelLoader : MonoBehaviour
{
    [SerializeField] private Button levelBtn;
    [SerializeField] private string levelName;

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
        // checking the levelStatus
        LevelStatus levelStatus = LevelManager.Instance.GetLevelStatus(levelName);

        switch (levelStatus)
        {
            case LevelStatus.Locked:
                Debug.Log("Can't play this level till you unlock it!");
                break;

            case LevelStatus.Unlocked:
                SceneManager.LoadScene(levelName);
                break;

            case LevelStatus.Completed:
                SceneManager.LoadScene(levelName);
                break;
        }
    }

    private void setLevelStatusOnButton()
    {
        levelBtn.interactable = LevelManager.Instance.GetLevelStatus(levelName) != LevelStatus.Locked;
    }
}
