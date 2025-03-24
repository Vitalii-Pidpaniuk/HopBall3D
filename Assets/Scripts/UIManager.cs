using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    private float _timeAlive;
    private int _intTimeAlive;
    public float fadeTime = 1f;
    
    public int finalScore;

    public int score;
    public int coins;
    
    [SerializeField] private GameObject stopScreen;
    [SerializeField] private GameObject optionPanel;
    public GameObject pausePanel;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI coinText;
    public  TextMeshProUGUI finalScoreText;
    public GameObject pauseButton;
    public GameObject perfectShotText;
    public GameObject loosePanel;
    public GameObject winPanel;
    public Sprite catchedFlower;
    public GameObject firstStar;
    public GameObject secondStar;
    public GameObject thirdStar;
    public GameObject newHighScorePanel;
    public CanvasGroup canvasGroup;
    public GameObject tutorial1;
    public GameObject tutorial2;
    public GameObject shopMenu;
    public GameObject shopButton;

    public List<Button> buyButtons1;
    public List<Button> buyButtons2;

    [SerializeField] private ShopManager shopManager;
    
    public void ShowStopScreen()
    {
        stopScreen.SetActive(true);
    }
    
    public void HideStopScreen()
    {
        stopScreen.SetActive(false);
    }

    public bool TouchInInteractebleZone()
    {
        Touch touch = Input.GetTouch(0);
        return touch.position.y < Display.main.renderingHeight * 0.8f;
    }
    
    public void ShowPanel(GameObject panel)
    {
        panel.SetActive(true);
    }

    public void HidePanel(GameObject panel)
    {
        pausePanel.SetActive(false);
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    
    public void ShowOptionPanel()
    {
        optionPanel.SetActive(true);
    }

    public void HideOptionPanel()
    {
        optionPanel.SetActive(false);
    }
    
    public void IncreaseScore()
    {
        _timeAlive += Time.fixedDeltaTime;
        _intTimeAlive = Mathf.FloorToInt(_timeAlive * 10);
        _intTimeAlive += score;
        finalScore = _intTimeAlive;
        scoreText.text = _intTimeAlive.ToString();
    }

    public void ShowShopMenu()
    {
        int coins = GameManager.Instance.GetCoins();
        int i = 1, j = 0;
        
        SetButtonActivity(buyButtons1);
        SetButtonActivity(buyButtons2);
        
        SetPrice(buyButtons1);
        SetPrice(buyButtons2);
        
        SetButtonInteraction(buyButtons1, coins);
        SetButtonInteraction(buyButtons2, coins);
        
        shopMenu.SetActive(true);   
    }
    
    public void HideShopMenu()
    {
        shopMenu.SetActive(false);
    }

    private void SetPrice(List<Button> list)
    {
        int i = 1;
        foreach (var button in list)
        {
            button.GetComponent<BuyButton>().SetCoins(i * 10);
            i++;
        }
    }
    
    private void SetButtonInteraction(List<Button> list, int coinsAmount)
    {
        foreach (var button in list)
        {
            if (coinsAmount < button.GetComponent<BuyButton>().GetCoins())
            {
                button.interactable = false;
            }
        }
    }
    
    public void SetButtonActivity(List<Button> list)
    {
        int i = 0;
        Dictionary<int, bool> purchaseList = shopManager.GetPurchases();
        if(purchaseList.Count > 0)
        {
            foreach (var purchase in purchaseList)
            {
                list[purchase.Key].gameObject.SetActive(false);
            }
        }
        shopManager.PrintPurchases();
    }
}
