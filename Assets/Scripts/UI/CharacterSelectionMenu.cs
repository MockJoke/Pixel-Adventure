using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class CharacterSelectionMenu : MonoBehaviour
{
    [SerializeField] private Canvas selectionCanvas;
    [SerializeField] private Image charImg;
    [SerializeField] private TextMeshProUGUI charNameText;
    [SerializeField] private Button selectBtn;
    [SerializeField] private TextMeshProUGUI selectBtnText;
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
        if (currChar == GetCurrCharIndex())
        {
            selectBtn.interactable = false;
            selectBtnText.text = "SELECTED";
        }
        else if (isCharAvailable(charData.characterData[currChar].charName))
        {
            selectBtn.interactable = true;
            selectBtnText.text = "SELECT";
        }
        else
        {
            selectBtn.interactable = false;
            selectBtnText.text = $"Unlocks at Level{GetCharUnlockLvl(currChar)}";
        }
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
        
        SetCharData();
    }
    
    private bool isCharAvailable(CharacterName characterName)
    {
        int charIndex = charData.characterData.FindIndex( data => data.charName == characterName);

        int lvl = GetCharUnlockLvl(charIndex);
        
        if (lvl <= LevelManager.Instance.GetLatestUnlockedLevelNo() + 1)
        {
            return true;
        }
        
        return false;
    }

    private int GetCharUnlockLvl(int charIndex)
    {
        return (int)charData.characterData[charIndex].unlocksAtLvl;
    }
}
