using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ExtraLivesMenu : MonoBehaviour
{
    [SerializeField] private Canvas canvas;
    
    [Space]
    [SerializeField] private int foodTradeFactor = 3;
    [SerializeField] private TextMeshProUGUI totalLifeCount;
    [SerializeField] private TextMeshProUGUI totalFoodCount;
    [SerializeField] private TextMeshProUGUI buyingLifeCount;
    [SerializeField] private TextMeshProUGUI deductingFoodCount;
    [SerializeField] private Button increaseLifeCountBtn;
    [SerializeField] private Button decreaseLifeCountBtn;
    
    [Space]
    [SerializeField] private CharacterDataSO charData;
     
    private int currLifeQty = 0;
    private int currFoodQty = 0;

    void Awake()
    {
        if (canvas == null)
            canvas = GetComponent<Canvas>();

        foodTradeFactor = charData.playerLifeData.FoodTradeForLivesFactor;
    }
    
    public void OpenMenu()
    {
        canvas.enabled = true;
        
        UpdateExtraLifeMenuUI();
        UpdateQtyBtnInteractivity();
    }

    public void CloseMenu()
    {
        canvas.enabled = false;
    }

    private void UpdateExtraLifeMenuUI()
    {
        totalLifeCount.text = $"{PlayerPrefs.GetInt("LifeCount", 0)}";
        totalFoodCount.text = $"{PlayerPrefs.GetInt("Foods"), 0}";
        buyingLifeCount.text = $"{currLifeQty}";
        deductingFoodCount.text = $"{currFoodQty}";
    }

    private void UpdateQtyBtnInteractivity()
    {
        decreaseLifeCountBtn.interactable = currLifeQty > 0;

        if (currLifeQty >= charData.playerLifeData.MaxLivesBound || Math.Abs(currFoodQty) > PlayerPrefs.GetInt("Foods", 0) - foodTradeFactor)
        {
            increaseLifeCountBtn.interactable = false;
        }
        else
        {
            increaseLifeCountBtn.interactable = true;
        }
    }
    
    public void IncreaseBuyingLifeCount()
    {
        currLifeQty++;
        currFoodQty -= 3;
        
        UpdateExtraLifeMenuUI();
        UpdateQtyBtnInteractivity();
    }
    
    public void DecreaseBuyingLifeCount()
    {
        if (currLifeQty <= 0)
            return;
        
        currLifeQty--;
        currFoodQty += 3;
        
        UpdateExtraLifeMenuUI();
        UpdateQtyBtnInteractivity();
    }

    public void GetExtraLives()
    {
        GiveLife(currLifeQty);
        
        int currFoodCount = PlayerPrefs.GetInt("Foods", 0);
        PlayerPrefs.SetInt("Foods", currFoodCount - Math.Abs(currFoodQty));
        
        ResetQty();
        
        UpdateExtraLifeMenuUI();
        UpdateQtyBtnInteractivity();
    }

    private void GiveLife(int count = 1)
    {
        int lifeCnt = PlayerPrefs.GetInt("LifeCount", 0);
        
        if (lifeCnt < charData.playerLifeData.MaxLivesBound)
        {
            lifeCnt += count;
        }
        
        PlayerPrefs.SetInt("LifeCount", lifeCnt);
    }

    private void ResetQty()
    {
        currLifeQty = 0;
        currFoodQty = 0;
    }
}
