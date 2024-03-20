using System;
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
    [SerializeField] private TextMeshProUGUI currCharText;
    [SerializeField] private Button selectBtn;
    
    [Space]
    [SerializeField] private UnityEvent onClose;
    
    private int currChar = 0;
    
    void Awake()
    {
        if (selectionCanvas == null)
            selectionCanvas = GetComponent<Canvas>();
    }

    void Start()
    {
        ShowCharacter(GetCurrCharIndex());
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
        
        SetSelectBtnInteractivity();
        
        SetCurrCharText();
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
        if (currChar != GetCurrCharIndex()) //GetCharStatus("A") == CharacterStatus.Unlocked)
        {
            selectBtn.interactable = true;
        }
        else
        {
            selectBtn.interactable = false;
        }
    }

    private void SetCurrCharText()
    {
        currCharText.gameObject.SetActive(currChar == GetCurrCharIndex());
    }
    #endregion

    public int GetCurrCharIndex()
    {
        return PlayerPrefs.GetInt("Character", 0);
    }
    
    public void SelectChar()
    {
        PlayerPrefs.SetInt("Character", currChar);
        
        SetSelectBtnInteractivity();
        
        SetCurrCharText();
    }
    
    private CharacterStatus GetCharStatus(string character)
    {
        CharacterStatus levelStatus = (CharacterStatus)PlayerPrefs.GetInt(character, 0);
        
        return levelStatus;
    }
}
