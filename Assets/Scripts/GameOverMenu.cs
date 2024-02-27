using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverMenu : MonoBehaviour
{
    [SerializeField] private Canvas gameOverCanvas;
    [SerializeField] private GameObject mainPanel;
    [SerializeField] private GameObject extraLifePanel;
    [SerializeField] private PlayerLife playerLife;

    [Header("Extra Life Panel Fields")] 
    [SerializeField] private TextMeshProUGUI totalLifeCount;
    [SerializeField] private TextMeshProUGUI totalFoodCount;
    [SerializeField] private TextMeshProUGUI buyingLifeCount;
    [SerializeField] private TextMeshProUGUI deductingFoodCount;
    [SerializeField] private Button increaseLifeCountBtn;
    [SerializeField] private Button decreaseLifeCountBtn;

    private int currLifeQty = 0;
    private int currFoodQty = 0;

    private void Awake()
    {
        if (gameOverCanvas == null)
            gameOverCanvas = GetComponent<Canvas>();
    }

    public void OpenMenu()
    {
        gameOverCanvas.enabled = true;
        SetTimeScale(false);
    }
    
    public void CloseMenu()
    {
        gameOverCanvas.enabled = false;
        SetTimeScale(true);
    }
    
    private void SetTimeScale(bool reset)
    {
        Time.timeScale = reset ? 1 : 0;
    }
    
    public void Home()
    {
        CloseMenu();
        SceneManager.LoadScene("Scenes/Start Screen");
    }
    
    public void StartAgain()
    {
        CloseMenu();
        playerLife.GiveLife(1);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void OpenExtraLifeMenu()
    {
        mainPanel.SetActive(false);
        extraLifePanel.SetActive(true);
        
        UpdateExtraLifeMenuUI();
        UpdateQtyBtnInteractivity();
    }
    
    public void CloseExtraLifeMenu()
    {
        extraLifePanel.SetActive(false);
        mainPanel.SetActive(true);
    }

    private void UpdateExtraLifeMenuUI()
    {
        totalLifeCount.text = $"{PlayerPrefs.GetInt("LifeCount")}";
        totalFoodCount.text = $"{PlayerPrefs.GetInt("Foods")}";
        buyingLifeCount.text = $"{currLifeQty}";
        deductingFoodCount.text = $"{currFoodQty}";
    }

    private void UpdateQtyBtnInteractivity()
    {
        decreaseLifeCountBtn.interactable = currLifeQty > 0;

        if (currLifeQty >= playerLife.maxLifeBound || Math.Abs(currFoodQty) > PlayerPrefs.GetInt("Foods") - 3)
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
}
