using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    
    private bool _onPause = false;
    private bool tutorialPassed = false;
    public bool onTutorial = false;
    private int _activeFlowerIndex;
    private bool _coinActive;
    private int _coins;
    private string _link = "https://www.youtube.com/watch?v=dQw4w9WgXcQ&ab_channel=RickAstley";
    public bool isGameMode;
    public bool isMainMenu;
    public List<GameObject> tales;
    public List<GameObject> mapTales;
    public List<GameObject> flowers;
    public GameObject talePrefab;
    public int scoreToWrite;
    public float timeSinceFlower = 0f;
    
    
    [SerializeField] private UIManager uiManager;
    [SerializeField] private BallMover ballMover;
    [SerializeField] private TilesGenerator tilesGenerator;
    [SerializeField] private MapGenerator mapGenerator;
    [SerializeField] private PlatformMover platformMover;
    [SerializeField] private MapMover mapMover;
    [SerializeField] private SoundManager soundManager;
    [SerializeField] private DataManager datamanager;
    [SerializeField] private PlatformEffector platformEffector;
    [SerializeField] private AnimationManager animationManager;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
    }

    private void Start()
    {
        Application.targetFrameRate = 60;
        if (isGameMode)
        {
            tilesGenerator.GenerateStartTiles(tales);
            tales[0].GetComponentInChildren<BoxCollider>().enabled = false;
            mapGenerator.GenerateStartTiles(mapTales, talePrefab);
            flowers.Add(uiManager.firstStar);
            flowers.Add(uiManager.secondStar);
            flowers.Add(uiManager.thirdStar);
            tutorialPassed = DataManager.GetTutorialState();
            if (!tutorialPassed)
            {
                StartCoroutine(ShowTutorial());
            }
        }
    }

    private void FixedUpdate()
    {
        if (isGameMode  && !onTutorial)
        {
            if (Input.touchCount > 0 && !_onPause && uiManager.TouchInInteractebleZone())
            {
                uiManager.HideStopScreen();
                uiManager.IncreaseScore();
                uiManager.coinText.text = _coins.ToString();
                soundManager.PlayTrack();
                ballMover.MoveBall();
                platformMover.MovePlatforms(tales);
                mapMover.MovePlatforms(mapTales);
                timeSinceFlower += Time.deltaTime;
            }
            else
            {
                soundManager.PauseTrack();
                uiManager.ShowStopScreen();
            }

            if (timeSinceFlower >= 10.0f && _activeFlowerIndex < 3)
            {
                SpawnFlower();
                Debug.Log("Flower spawned!");
                timeSinceFlower = 0f;
            }
            if (timeSinceFlower >= 10.0f && _activeFlowerIndex == 3 && !_coinActive)
            {
                SpawnCoin();
                Debug.Log("Coin spawned!");
                timeSinceFlower = 0f;
            }
        }
    }

    private void Update()
    {
        // Debug.Log("FPS: " + 1.0f/Time.deltaTime);
    }

    private IEnumerator ShowTutorial()
    {
        animationManager.tutorialCanvas.SetActive(true);
        onTutorial = true;
        uiManager.HideStopScreen();
        uiManager.tutorial1.SetActive(true);
        animationManager.PanelFadeIn(animationManager.tutorialCanvasGroup);
    
        yield return new WaitUntil(() => Input.touchCount > 0);
        yield return new WaitUntil(() => Input.touchCount == 0);
    
        //StartCoroutine(HidePanelDelayed(uiManager.tutorial1, 1f));
        animationManager.PanelFadeOut(animationManager.tutorialCanvasGroup);
        yield return new WaitForSeconds(1f);
        uiManager.tutorial1.SetActive(false);
    
        uiManager.tutorial2.SetActive(true);
        animationManager.PanelFadeIn(animationManager.tutorialCanvasGroup);
    
        yield return new WaitUntil(() => Input.touchCount > 0);
        yield return new WaitUntil(() => Input.touchCount == 0);
    
        //StartCoroutine(HidePanelDelayed(uiManager.tutorial2, 1f));
        animationManager.PanelFadeOut(animationManager.tutorialCanvasGroup);
        yield return new WaitForSeconds(1f);
        uiManager.tutorial2.SetActive(false);
        
        DataManager.SaveTutorialPassed();
        uiManager.ShowStopScreen();
        //animationManager.tutorialCanvasGroup.gameObject.SetActive(false);
        animationManager.tutorialCanvas.SetActive(false);
        onTutorial = false;
    }


    private IEnumerator HidePanelDelayed(GameObject panel, float time)
    {
        yield return new WaitForSeconds(time);
        uiManager.HidePanel(panel);
    }
    
    public void Pause()
    {
        _onPause = true;
        uiManager.ShowPanel(uiManager.pausePanel);
        animationManager.PanelJumpFadeIn(uiManager.pausePanel);
    }

    public void Continue()
    {
        _onPause = false;
        SoundManager.Instance.musicChannel.Play();
        animationManager.PanelJumpFadeOut(uiManager.pausePanel);
        StartCoroutine(HidePanelDelayed(uiManager.pausePanel, 1f));
    }

    public void Restart()
    {
        uiManager.Restart();
    }

    public void Options()
    {
        uiManager.ShowOptionPanel();
    }

    public void Quit()
    {
        SceneManager.LoadScene("Levels");
    }
    
    public void Back()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }

    public void NextScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void Death()
    {
        _onPause = true;
        uiManager.pauseButton.SetActive(false);
        SoundManager.Instance.FallSfx();
        (int loadedStars, int loadedScore) = DataManager.LoadLevelProgress(SceneManager.GetActiveScene().buildIndex - 3);
        if (_activeFlowerIndex > 0)
        {
            uiManager.finalScoreText.text += uiManager.scoreText.text;
            uiManager.winPanel.SetActive(true); 
            animationManager.PanelJumpFadeIn(uiManager.winPanel);
        }
        else
        {
            uiManager.loosePanel.SetActive(true);   
            animationManager.PanelJumpFadeIn(uiManager.loosePanel);
        }

        if (loadedScore < uiManager.finalScore)
        {
            loadedScore = uiManager.finalScore;
            uiManager.newHighScorePanel.SetActive(true);
        }
        
        DataManager.SaveLevelProgress(SceneManager.GetActiveScene().buildIndex - 3, _activeFlowerIndex, loadedScore);
        DataManager.SaveCoins(_coins);
    }

    public void PerfectShot()
    {
        uiManager.score += 100;
        ShowPerfectShot();
        //StartCoroutine(HidePerfectShot());
        //animationManager.PanelFadeOut(animationManager.perfectShotCanvasGroup);
        //StartCoroutine(HidePanelDelayed(uiManager.perfectShotText, 1f));
    }

    private void ShowPerfectShot()
    {
        uiManager.perfectShotText.SetActive(true);
        animationManager.PanelFadeIn(animationManager.perfectShotCanvasGroup);
        StartCoroutine(HidePerfectShot());
    }
    
    private IEnumerator HidePerfectShot()
    {
        yield return new WaitForSeconds(0.7f);
        animationManager.PanelFadeOut(animationManager.perfectShotCanvasGroup);
        //StartCoroutine(HidePanelDelayed(uiManager.perfectShotText, 1f));
    }

    // private void ShowPerfectShot()
    // {
    //     uiManager.perfectShotText.SetActive(true);
    //     animationManager.PanelFadeIn(animationManager.perfectShotCanvasGroup);
    //     StartCoroutine(HidePerfectShot());
    // }
    //
    // private IEnumerator HidePerfectShot()
    // {
    //     animationManager.PanelFadeOut(animationManager.perfectShotCanvasGroup);
    //     yield return new WaitForSeconds(1f);
    //     StartCoroutine(HidePanelDelayed(uiManager.perfectShotText, 1f));
    // }
    
    private void SpawnFlower()
    {
        tales[0].GetComponent<Flower>().ActiveFlower();
    }
    private void SpawnCoin()
    {
        _coinActive = true;
        tales[0].GetComponent<Coin>().ActiveCoin();
    }

    public void FlowerCatched()
    {
        flowers[_activeFlowerIndex].GetComponent<Image>().sprite = uiManager.catchedFlower;
        flowers[_activeFlowerIndex].GetComponent<Image>().color = new Vector4(255,255,255,255);
        SoundManager.Instance.FlowerCatchSfx();
        _activeFlowerIndex++;
    }
    
    public void CoinCatched()
    {
        SoundManager.Instance.FlowerCatchSfx();
        _coinActive = false;
        _coins++;
    }

    public void LoadLevel(int n)
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + n + 1);
    }

    public void Music()
    {
        soundManager.ChangeMusicState();
    }
    
    public void SFX()
    {
        soundManager.ChangeSfxState();
    }

    public void OpenLink()
    {
        Application.OpenURL(_link);
    }

    public void CloseGame()
    {
        Application.Quit();
    }

    public void PlatformExplode(PlatformEffector platformEffector)
    {
        platformEffector.Shatter();
    }

    public void ShowShop()
    {
        uiManager.ShowShopMenu();
    }

    public void HideShop()
    {
        uiManager.HideShopMenu();
    }

    public int GetCoins()
    {
        return DataManager.LoadCoins();
    }
}
