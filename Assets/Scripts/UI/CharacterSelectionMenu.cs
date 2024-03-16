using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class CharacterSelectionMenu : MonoBehaviour
{
    [SerializeField] private Canvas selectionCanvas;
    [SerializeField] private Image charImg; 
    [SerializeField] private Sprite[] CharSprites;
    
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
        
        PlayerPrefs.SetInt("CurrentCar", currChar);
    }

    public void ShowNext()
    {
        ShowCharacter(currChar + 1);
    }

    public void ShowPrevious()
    {
        ShowCharacter(currChar - 1);
    }
}
