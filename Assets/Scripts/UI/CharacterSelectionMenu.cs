using System;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class CharacterSelectionMenu : MonoBehaviour
{
    [SerializeField] private Canvas selectionCanvas;
    [SerializeField] private Image charImg;
    [SerializeField] private TextMeshProUGUI currCharText;
    [SerializeField] private TextMeshProUGUI charNameText;
    [SerializeField] private TextMeshProUGUI charStatusText;
    [SerializeField] private Button selectBtn;
    [SerializeField] private CharacterDataSO charData;
    
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
        if(charNo > charData.characterData.Count - 1)
        {
            charNo = 0;
        }
        else if(charNo < 0)
        {
            charNo = charData.characterData.Count - 1;
        }
        
        currChar = charNo;

        charImg.sprite = charData.characterData[currChar].charSprite;
        
        SetSelectBtnInteractivity();
        
        SetCurrCharText();
        
        SetCharData();
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

    private void SetCharData()
    {
        charNameText.text = charData.characterData[currChar].charName.ToString();
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
        
        SetCharData();
    }
    
    private CharacterStatus GetCharStatus(string character)
    {
        CharacterStatus levelStatus = (CharacterStatus)PlayerPrefs.GetInt(character, 0);
        
        return levelStatus;
    }
}
