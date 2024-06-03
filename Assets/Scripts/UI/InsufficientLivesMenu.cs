using UnityEngine;

public class InsufficientLivesMenu : MonoBehaviour
{
    [SerializeField] private Canvas canvas;
    [SerializeField] private GameObject mainPanel;

    void Awake()
    {
        if (canvas == null)
            canvas = GetComponent<Canvas>();
    }

    public void OpenMenu()
    {
        canvas.enabled = true;
    }
    
    public void CloseMenu()
    {
        canvas.enabled = false;
    }
    
    public void OnExtraLivesMenuOpen()
    {
        mainPanel.SetActive(false);
    }
    
    public void OnExtraLivesMenuClose()
    {
        mainPanel.SetActive(true);
    }
}
