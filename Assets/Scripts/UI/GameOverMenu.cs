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
    [SerializeField] private Button startAgainBtn;

    [Header("Extra Life Panel Fields")] 
    [SerializeField] private int foodTradeFactor = 3;
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

        if (playerLife == null)
            playerLife = FindObjectOfType<PlayerLife>();
    }

    public void OpenGameOverMenu()
    {
        gameOverCanvas.enabled = true;
        SetTimeScale(false);

        SetStartAgainBtn();
    }
    
    public void CloseGameOverMenu()
    {
        gameOverCanvas.enabled = false;
        SetTimeScale(true);
    }

    private void SetStartAgainBtn()
    {
        startAgainBtn.gameObject.SetActive(PlayerPrefs.GetInt("LifeCount") > 0);
    }
    
    private void SetTimeScale(bool reset)
    {
        Time.timeScale = reset ? 1 : 0;
    }
    
    public void Home()
    {
        CloseGameOverMenu();
        SceneManager.LoadScene("Scenes/Start Screen");
    }
    
    public void StartAgain()
    {
        CloseGameOverMenu();
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
        
        SetStartAgainBtn();
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

        if (currLifeQty >= playerLife.maxLifeBound || Math.Abs(currFoodQty) > PlayerPrefs.GetInt("Foods") - foodTradeFactor)
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
        playerLife.GiveLife(currLifeQty);
        
        int currFoodCount = PlayerPrefs.GetInt("Foods");
        PlayerPrefs.SetInt("Foods", currFoodCount - Math.Abs(currFoodQty));
        
        ResetQty();
        
        UpdateExtraLifeMenuUI();
        UpdateQtyBtnInteractivity();
    }

    private void ResetQty()
    {
        currLifeQty = 0;
        currFoodQty = 0;
    }
}
