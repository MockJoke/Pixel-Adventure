using UnityEngine;
using UnityEngine.UI;

public class InsufficientLivesMenu : MonoBehaviour
{
    [SerializeField] private Canvas canvas;
    [SerializeField] private GameObject mainPanel;
    [SerializeField] private Button startAgainBtn;
    
    // [Space]
    // [SerializeField] private UnityEvent onClose;

    void Awake()
    {
        if (canvas == null)
            canvas = GetComponent<Canvas>();
    }

    public void OpenMenu()
    {
        canvas.enabled = true;
        
        // SetStartAgainBtn();
    }
    
    public void CloseMenu()
    {
        canvas.enabled = false;
        // onClose?.Invoke();
    }
    
    private void SetStartAgainBtn()
    {
        startAgainBtn.gameObject.SetActive(PlayerPrefs.GetInt("LifeCount") < 0);
    }
    
    public void OnExtraLivesMenuOpen()
    {
        mainPanel.SetActive(false);
    }
    
    public void OnExtraLivesMenuClose()
    {
        mainPanel.SetActive(true);
        
        // SetStartAgainBtn();
    }
}
