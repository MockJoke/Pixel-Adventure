using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class CharacterSelectionMenu : MonoBehaviour
{
    [SerializeField] private Canvas selectionCanvas;
    [SerializeField] private Image charImg; 
    [SerializeField] private Sprite[] CharSprites;
    [SerializeField] private TextMeshProUGUI charStatusText;
    [SerializeField] private Button selectBtn;
    
    [Space]
    [SerializeField] private UnityEvent onClose;
    
    private int currChar = 0;
    
    void Awake()
    {
        if (selectionCanvas == null)
            selectionCanvas = GetComponent<Canvas>();
    }
    
    public void OpenMenu()
    {
        selectionCanvas.enabled = true;
    }

    public void CloseMenu()
    {
        selectionCanvas.enabled = false;
        onClose?.Invoke();
    }

    #region Character Navigation UI
    private void ShowCharacter(int charNo)
    {
        if(charNo > CharSprites.Length - 1)
        {
            charNo = 0;
        }
        else if(charNo < 0)
        {
            charNo = CharSprites.Length - 1;
        }
        
        currChar = charNo;

        charImg.sprite = CharSprites[currChar];
        
        // SetSelectBtnInteractivity();
    }

    public void ShowNext()
    {
        ShowCharacter(currChar + 1);
    }

    public void ShowPrevious()
    {
        ShowCharacter(currChar - 1);
    }

    private void SetSelectBtnInteractivity()
    {
        if (GetCharStatus("A") == CharacterStatus.Unlocked)
        {
            selectBtn.interactable = true;
        }
        else
        {
            selectBtn.interactable = false;
        }
    }
    #endregion

    public void SelectChar()
    {
        PlayerPrefs.SetInt("Character", currChar);
    }
    
    private CharacterStatus GetCharStatus(string character)
    {
        CharacterStatus levelStatus = (CharacterStatus)PlayerPrefs.GetInt(character, 0);
        
        return levelStatus;
    }
}
