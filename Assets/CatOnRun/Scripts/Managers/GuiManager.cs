using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine;
using GoogleMobileAds.Api;
using System;


[System.Serializable]
public class MainMenu
{
    public Button playBtn, soundBtn;
}
[System.Serializable]
public class GameOverMenu
{
    public Button homeBtn, retryBtn, scoreSendBtn, showBordBtn;
    public Image starImage;
    public Text goScoreText, goBestText, starText;
}
[System.Serializable]
public class GameMenu
{
    //public Button pauseBtn;

    public Button resumeBtn, homeBtn, retryBtn;
    public Image starImage;
    public Text scoreText, starText;
}

public class GuiManager : MonoBehaviour
{
    public GameObject tutorialPanel;
    public GameObject pauseBtn;
    public GameObject gameStartBtn;
    public bool tutorial;

    //#region AdMob

    //private GameObject adMobRectangleObj;
    ////private AdMobRectangle adMobRectangleScript;
    //private GameObject adMobInterstitialObj;

    //public GameObject adRectangleBg;

    ////レクタングル用ランダム値
    //int rectangleRandomNum;
    ////インタースティシャル用ランダム値
    //int interstitialRandomNum;

    //bool rectangleStandby;

    //#endregion

    public Button gameCenterLoginBtn;

    public static GuiManager instance;

    public Text shopStarText, worldStarText;
    public Image shopStartImage, worldStarImage;
    public AudioSource musicAudio, btnClickAudio;
    public MainMenu mainMenu;
    public GameOverMenu gameOverMenu;
    public GameMenu gameMenu;
    public bool gameStarted = false;
    public int multiplier = 1;

    public GameObject mainMenuPanel, gameMenuPanel, gameoverMenuPanel, inGamePanel, pausePanel, worldMenu;
    public Text[] mainFont, secondFont;

    private float scoreCounter;
    //private int score, currentPoints;
    public int _highScore, currentPoints;

    [HideInInspector]
    public managerVars vars;

    void OnEnable()
    {
        vars = Resources.Load("managerVarsContainer") as managerVars;
    }

    void Awake()
    {
        if (instance == null)
            instance = this;
    }

    // Use this for initialization
    void Start()
    {
        //adMobRectangleObj = GameObject.Find("AdMobRectangle");
        //adMobInterstitialObj = GameObject.Find("AdMobInterstitial");
        //adMobRectangleScript = adMobRectangleObj.GetComponent<AdMobRectangle>();

        //int resultAdsFalse = AdMobRectangle.GetAdsFalse();

        AssignBtnMethods();
        Initialize();

        //Debug.Log("GuiManager resultAdsFalse" + resultAdsFalse);

        //#region test
        //adMobRectangleObj.GetComponent<AdMobRectangle>().RequestBanner();
        //DelayBannnerShow();
        //#endregion
    }

    //void DelayBannnerShow()
    //{
    //    // レクタングルバナーBG表示
    //    adRectangleBg.SetActive(true);
    //    // ロード済みのレクタングル広告を表示する
    //    adMobRectangleObj.GetComponent<AdMobRectangle>().BannerShow();
    //    Debug.Log("ロード済みレクタングル広告を表示");
    //}

    // レクタングルバナーBG閉じる
    //public void AdRectanglebgClose()
    //{
    //    adRectangleBg.SetActive(false);
    //    adMobRectangleObj.GetComponent<AdMobRectangle>().BannerDestroy();
    //    rectangleStandby = false;
    //    ButtonEnabled();
    //}


    //ゲームスタート
    public void GameStart()
    {
        // 未課金(0)の場合はレクタングル用ランダム値を決める
        //if (AdMobRectangle.GetAdsFalse() == 0)
        //{
        //    //レクタングル用ランダム値 1~5
        //    rectangleRandomNum = UnityEngine.Random.Range(1, 6);
        //    Debug.Log(("レクタングル用ランダム値") + rectangleRandomNum.ToString());
        //    Debug.Log("AdsFalse" + AdMobRectangle.GetAdsFalse());

        //    if (rectangleRandomNum >= 4)
        //    {
        //        Debug.Log("レクタングル用ランダム値" + rectangleRandomNum + "以上の為レクタングルロード");
        //        rectangleStandby = true;
        //        adMobRectangleObj.GetComponent<AdMobRectangle>().RequestBanner();
        //    }
        //    else
        //    {
        //        Debug.Log("レクタングル用ランダム値" + rectangleRandomNum + "以下の為なにもしない");
        //    }
        //}
        //// 課金済(1)の場合
        //else
        //{
        //    Debug.Log("課金済みのためレクタングル用ランダム値は決めない。ロード済みインタースティシャル削除 AdsFalse" + AdMobRectangle.GetAdsFalse());
        //    adMobInterstitialObj.GetComponent<AdMobInterstitial>().InterstitialClose();
        //}


        ButtonPress();

        tutorial = true;

        pauseBtn.SetActive(true);
        gameStartBtn.SetActive(false);
        tutorialPanel.SetActive(false);
        Time.timeScale = 1;
    }


    void Initialize()
    {
#if AdmobDef
        //if (GameManager.instance.canShowAds) AdsManager.instance.ShowBannerAds();
        //else if (!GameManager.instance.canShowAds) AdsManager.instance.DestroyBannerAds();
#endif
        //sound button
        if (GameManager.instance.isMusicOn == true)
        {
            AudioListener.volume = 1;
            mainMenu.soundBtn.image.sprite = vars.soundOffButton;
        }
        else
        {
            AudioListener.volume = 0;
            mainMenu.soundBtn.image.sprite = vars.soundOnButton;
        }

        //if (Time.timeScale == 0)
        //Time.timeScale = 1;

        if (GameManager.instance.gameRestart)
        {
            GameManager.instance.gameRestart = false;
            mainMenuPanel.SetActive(false);
            PlayBtn();
        }

        scoreCounter = 0;
        _highScore = 0;
        currentPoints = 0;
        gameMenu.scoreText.text = "" + _highScore;

        gameMenu.starText.text = "" + currentPoints;
        gameOverMenu.starText.text = "+" + currentPoints;
        shopStarText.text = "" + GameManager.instance.points;
        worldStarText.text = "" + GameManager.instance.points;
        GameManager.instance.currentPoints = 0;
        GameManager.instance.tileSetChanged = false;
        musicAudio.clip = vars.backgroundMusic;
        musicAudio.Play();
    }

    // Update is called once per frame
    void Update()
    {
        if (!GameManager.instance.gameOver && gameStarted)
        {
            scoreCounter += (10 * Time.deltaTime) * multiplier;
            _highScore = Mathf.RoundToInt(scoreCounter);
            gameMenu.scoreText.text = "" + _highScore;
            currentPoints = _highScore / 10;
            if (currentPoints > GameManager.instance.currentPoints)
            {
                btnClickAudio.PlayOneShot(vars.starSound);
                GameManager.instance.currentPoints = currentPoints;
            }
            gameMenu.starText.text = "" + currentPoints;
        }

        shopStarText.text = "" + GameManager.instance.points;
        worldStarText.text = "" + GameManager.instance.points;

    }

    //各ボタンの設定
    void AssignBtnMethods()
    {
        #region MainMenu
        mainMenu.soundBtn.GetComponent<Button>().onClick.AddListener(() => { SoundBtn(); });    //sound
        //mainMenu.facebookBtn.GetComponent<Button>().onClick.AddListener(() => { FacebookBtn(); });    //facebook
        //mainMenu.twitterBtn.GetComponent<Button>().onClick.AddListener(() => { TwitterBtn(); });    //twitter
        //mainMenu.noAdsBtn.GetComponent<Button>().onClick.AddListener(() => { NoAdsBtn(); });    //noAds
        //mainMenu.leaderboardBtn.GetComponent<Button>().onClick.AddListener(() => { LeaderboardBtn(); });//leaderboard
        //mainMenu.rateBtn.GetComponent<Button>().onClick.AddListener(() => { RateBtn(); });//rate
        //mainMenu.moreGamesBtn.GetComponent<Button>().onClick.AddListener(() => { MoreGamesBtn(); });//more games
        #endregion

        #region GameMenu
        gameMenu.homeBtn.GetComponent<Button>().onClick.AddListener(() => { PuaseToHome(); });
        gameMenu.retryBtn.GetComponent<Button>().onClick.AddListener(() => { PausetoResume(); });
        #endregion

        #region GameOver
        gameOverMenu.homeBtn.GetComponent<Button>().onClick.AddListener(() => { HomeBtn(); });
        gameOverMenu.retryBtn.GetComponent<Button>().onClick.AddListener(() => { RetryBtn(); });
        //gameOverMenu.shareBtn.GetComponent<Button>().onClick.AddListener(() => { ShareBtn(); });
        #endregion
    }

    #region MainMenu

    //各種ボタン無効化
    void ButtonDisable()
    {
        gameOverMenu.homeBtn.GetComponent<Button>().enabled = false;
        gameOverMenu.retryBtn.GetComponent<Button>().enabled = false;
        gameOverMenu.scoreSendBtn.GetComponent<Button>().enabled = false;
        gameOverMenu.showBordBtn.GetComponent<Button>().enabled = false;
    }

    //各種ボタン有効化
    void ButtonEnabled()
    {
        gameOverMenu.homeBtn.GetComponent<Button>().enabled = true;
        gameOverMenu.retryBtn.GetComponent<Button>().enabled = true;
        gameOverMenu.scoreSendBtn.GetComponent<Button>().enabled = true;
        gameOverMenu.showBordBtn.GetComponent<Button>().enabled = true;
    }

    //ステージ選択画面からチュートリアル画面表示
    public void PlayBtn()
    {
        ButtonPress();

        tutorial = false;

        pauseBtn.SetActive(false);
        worldMenu.SetActive(false);
        gameMenuPanel.SetActive(true);

        gameStarted = true;
        Time.timeScale = 0;
        PlatformSpawner.instance.Reset();
        CameraController.instance.Reset();
    }



    void SoundBtn()
    {
        ButtonPress();
        if (GameManager.instance.isMusicOn == true)
        {
            GameManager.instance.isMusicOn = false;
            AudioListener.volume = 0;
            mainMenu.soundBtn.image.sprite = vars.soundOnButton;
            GameManager.instance.Save();
        }
        else
        {
            GameManager.instance.isMusicOn = true;
            AudioListener.volume = 1;
            mainMenu.soundBtn.image.sprite = vars.soundOffButton;
            GameManager.instance.Save();
        }
    }

    public void GameCenterLoginOpen()
    {
        gameCenterLoginBtn.gameObject.SetActive(true);
    }

    public void GamecenterLoginClose()
    {
        gameCenterLoginBtn.gameObject.SetActive(false);
    }

    //public void RandomNumInterstitial()
    //{
    //    // 未課金(0)の場合はインタースティシャル用ランダム値を決める
    //    if (AdMobRectangle.GetAdsFalse() == 0)
    //    {
    //        //インタースティシャル用ランダム値 1~6
    //        interstitialRandomNum = UnityEngine.Random.Range(1, 7);
    //        Debug.Log(("インタースティシャル用ランダム値") + interstitialRandomNum.ToString());

    //        if (interstitialRandomNum >= 5)
    //        {
    //            Debug.Log("インタースティシャル用ランダム値" + interstitialRandomNum + "以上の為インタースティシャル表示");
    //            adMobInterstitialObj.GetComponent<AdMobInterstitial>().InterstitialShow();
    //        }
    //        else
    //        {
    //            Debug.Log("インタースティシャル用ランダム値" + interstitialRandomNum + "以下の為なにもしない");
    //            string sceneName = SceneManager.GetActiveScene().name;
    //            SceneManager.LoadScene(sceneName);
    //        }
    //    }
    //    // 課金済(1)の場合
    //    else
    //    {
    //        string sceneName = SceneManager.GetActiveScene().name;
    //        SceneManager.LoadScene(sceneName);
    //        Debug.Log("課金済みのため何もしない AdsFalse" + AdMobRectangle.GetAdsFalse());
    //    }
    //}

    //    void NoAdsBtn()
    //    {
    //        ButtonPress();
    //        //Purchaser.instance.BuyNoAds();
    //    }

    //    void FacebookBtn()
    //    {
    //        ButtonPress();
    //        Application.OpenURL(vars.facebookPage);
    //    }

    //    void LeaderboardBtn()
    //    {
    //        ButtonPress();
    //#if UNITY_ANDROID
    //        GooglePlayManager.singleton.OpenLeaderboardsScore();
    //#elif UNITY_IOS
    //        LeaderboardiOSManager.instance.ShowLeaderboard();
    //#endif
    //}

    //void TwitterBtn()
    //{
    //    ButtonPress();
    //    Application.OpenURL(vars.twitterPage);
    //}

    //void RateBtn()
    //{
    //    ButtonPress();
    //    Application.OpenURL(vars.rateButtonUrl);
    //}

    //void MoreGamesBtn()
    //{
    //    ButtonPress();
    //    Application.OpenURL(vars.moreGamesUrl);
    //}

    #endregion

    #region GameOver
    void HomeBtn()
    {
        //RandomNumInterstitial();
        ButtonPress();
        GameManager.instance.gameOver = false;
        string sceneName = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(sceneName);
        Debug.Log("Game Over to Home");
    }

    void RetryBtn()
    {
        //RandomNumInterstitial();
        ButtonPress();
        GameManager.instance.gameRestart = true;
        GameManager.instance.gameOver = false;
        Debug.Log("Game Over to Retry");
        string sceneName = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(sceneName);
    }

    //void ShareBtn()
    //{
    //    ButtonPress();
    //    ShareScreenShot.instance.ShareMethode();
    //}

    public void ScoreSendSuccess()
    {
        gameOverMenu.scoreSendBtn.gameObject.SetActive(false);
    }

    #endregion

    #region GameMenu

    void ResumeBtn()
    {
        ButtonPress();
        Time.timeScale = 1;
        pausePanel.SetActive(false);
    }

    public void PauseBtn()
    {
        ButtonPress();
        Time.timeScale = 0;
        pausePanel.SetActive(true);
    }

    //ポーズメニューからゲーム再開      
    public void PausetoResume()
    {
        pauseBtn.SetActive(true);
        ButtonPress();
        ResumeBtn();
        Debug.Log("Pause to Resume");
    }

    //ポーズメニューからホームへ戻る      
    public void PuaseToHome()
    {
        ButtonPress();
        string sceneName = SceneManager.GetActiveScene().name;
        GameManager.instance.gameOver = false;
        SceneManager.LoadScene(sceneName);
        Time.timeScale = 1;
        Debug.Log("Pause to Home");
    }

    #endregion

    public void ButtonPress()
    {
        btnClickAudio.PlayOneShot(vars.buttonSound);
    }

    public void GameOver()
    {
        //ボタン無効化
        //ButtonDisable();
        //n秒後にボタン有効化
        //Invoke("ButtonEnabled", 2.5f);

        //trueならn秒後にレクタングル表示         
        //if (rectangleStandby == true)
        //{
        //    //ボタン無効化
        //    ButtonDisable();
        //    Invoke("DelayBannnerShow", 2f);
        //}

        //if (musicAudio.isPlaying)
        //{
        //    musicAudio.Stop();
        //}

        btnClickAudio.PlayOneShot(vars.deathSound);

        gameOverMenu.goScoreText.text = "" + _highScore;
        if (_highScore > GameManager.instance.bestScore)
        {
            GameManager.instance.bestScore = _highScore;
        }
        gameOverMenu.goBestText.text = "" + GameManager.instance.bestScore;
        gameOverMenu.starText.text = "" + currentPoints;
        GameManager.instance.lastScore = _highScore;
        GameManager.instance.points += currentPoints;
        gameMenuPanel.SetActive(false);
        gameoverMenuPanel.SetActive(true);

        Debug.Log("GAME OVER!");
        GameManager.instance.Save();

    }

}
