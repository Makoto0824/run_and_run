using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine;
using System;


[System.Serializable]
public class MainMenu
{
    public Button playBtn, soundBtn;
}
[System.Serializable]
public class GameOverMenu
{
    public Button homeBtn, retryBtn;
    public Image starImage;
    public Text goScoreText, goBestText, starText;
}
[System.Serializable]
public class GameMenu
{
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

    void Start()
    {
        AssignBtnMethods();
        Initialize();
    }

    public void GameStart()
    {
        ButtonPress();

        worldMenu.SetActive(false);
        inGamePanel.SetActive(true);
        gameMenuPanel.SetActive(true);
        tutorialPanel.SetActive(true);
        gameStartBtn.SetActive(true);

        Time.timeScale = 0;
        tutorial = false;
        gameStarted = false;
        GameManager.instance.gameOver = false;

        // カメラの動きを開始
        if (CameraController.instance != null)
        {
            CameraController.instance.StartMoving();
        }

        // プレイヤーの動きを開始
        if (PlayerController.instance != null)
        {
            PlayerController.instance.StartMoving();
        }
    }

    public void StartGame()
    {
        ButtonPress();
        ButtonDisable();

        tutorial = true;
        gameStarted = true;

        pauseBtn.SetActive(true);
        gameStartBtn.SetActive(false);
        tutorialPanel.SetActive(false);
        Time.timeScale = 1;
    }

    void Initialize()
    {
        if (GameManager.instance.isMusicOn)
        {
            AudioListener.volume = 1;
            if (mainMenu != null && mainMenu.soundBtn != null && mainMenu.soundBtn.image != null)
                mainMenu.soundBtn.image.sprite = vars.soundOffButton;
        }
        else
        {
            AudioListener.volume = 0;
            if (mainMenu != null && mainMenu.soundBtn != null && mainMenu.soundBtn.image != null)
                mainMenu.soundBtn.image.sprite = vars.soundOnButton;
        }

        if (GameManager.instance.gameRestart)
        {
            GameManager.instance.gameRestart = false;
            if (mainMenuPanel != null)
                mainMenuPanel.SetActive(false);
            PlayBtn();
        }

        scoreCounter = 0;
        _highScore = 0;
        currentPoints = 0;

        if (gameMenu != null)
        {
            if (gameMenu.scoreText != null)
                gameMenu.scoreText.text = $"{_highScore}";

            if (gameMenu.starText != null)
                gameMenu.starText.text = $"{currentPoints}";
        }

        if (shopStarText != null)
            shopStarText.text = $"{GameManager.instance.points}";

        if (worldStarText != null)
            worldStarText.text = $"{GameManager.instance.points}";
    }

    void Update()
    {
        if (gameStarted)
        {
            scoreCounter += Time.deltaTime * multiplier;
            _highScore = (int)scoreCounter;
            gameMenu.scoreText.text = $"{_highScore}";
        }
    }

    void AssignBtnMethods()
    {
        if (mainMenu != null)
        {
            if (mainMenu.playBtn != null)
                mainMenu.playBtn.onClick.AddListener(PlayBtn);

            if (mainMenu.soundBtn != null)
                mainMenu.soundBtn.onClick.AddListener(SoundBtn);
        }

        if (gameOverMenu != null)
        {
            if (gameOverMenu.homeBtn != null)
                gameOverMenu.homeBtn.onClick.AddListener(HomeBtn);

            if (gameOverMenu.retryBtn != null)
                gameOverMenu.retryBtn.onClick.AddListener(RetryBtn);
        }

        if (gameMenu != null)
        {
            if (gameMenu.homeBtn != null)
                gameMenu.homeBtn.onClick.AddListener(HomeBtn);

            if (gameMenu.retryBtn != null)
                gameMenu.retryBtn.onClick.AddListener(RetryBtn);

            if (gameMenu.resumeBtn != null)
                gameMenu.resumeBtn.onClick.AddListener(ResumeBtn);
        }

        if (gameStartBtn != null)
        {
            gameStartBtn.GetComponent<Button>().onClick.AddListener(StartGame);
        }
    }

    void ButtonDisable()
    {
        if (mainMenu != null)
        {
            if (mainMenu.playBtn != null)
                mainMenu.playBtn.interactable = false;

            if (mainMenu.soundBtn != null)
                mainMenu.soundBtn.interactable = false;
        }
    }

    void ButtonEnabled()
    {
        if (mainMenu != null)
        {
            if (mainMenu.playBtn != null)
                mainMenu.playBtn.interactable = true;

            if (mainMenu.soundBtn != null)
                mainMenu.soundBtn.interactable = true;
        }
    }

    public void PlayBtn()
    {
        ButtonPress();
        ButtonDisable();

        mainMenuPanel.SetActive(false);
        worldMenu.SetActive(true);
    }

    void SoundBtn()
    {
        ButtonPress();

        if (GameManager.instance.isMusicOn)
        {
            GameManager.instance.isMusicOn = false;
            AudioListener.volume = 0;
            if (mainMenu != null && mainMenu.soundBtn != null && mainMenu.soundBtn.image != null)
                mainMenu.soundBtn.image.sprite = vars.soundOnButton;
        }
        else
        {
            GameManager.instance.isMusicOn = true;
            AudioListener.volume = 1;
            if (mainMenu != null && mainMenu.soundBtn != null && mainMenu.soundBtn.image != null)
                mainMenu.soundBtn.image.sprite = vars.soundOffButton;
        }

        GameManager.instance.Save();
    }

    void HomeBtn()
    {
        ButtonPress();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    void RetryBtn()
    {
        ButtonPress();

        GameManager.instance.gameRestart = true;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    void ResumeBtn()
    {
        ButtonPress();
        pausePanel.SetActive(false);
        Time.timeScale = 1;
    }

    public void PauseBtn()
    {
        ButtonPress();
        pausePanel.SetActive(true);
        Time.timeScale = 0;
    }

    public void PausetoResume()
    {
        ButtonPress();
        pausePanel.SetActive(false);
        Time.timeScale = 1;
    }

    public void PuaseToHome()
    {
        ButtonPress();
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void ButtonPress()
    {
        btnClickAudio.Play();
    }

    public void GameOver()
    {
        gameStarted = false;
        GameManager.instance.gameOver = true;

        gameMenuPanel.SetActive(false);
        gameoverMenuPanel.SetActive(true);

        gameOverMenu.goScoreText.text = "" + _highScore;
        gameOverMenu.goBestText.text = "" + GameManager.instance.bestScore;

        if (_highScore > GameManager.instance.bestScore)
        {
            GameManager.instance.bestScore = _highScore;
            GameManager.instance.Save();
        }

        GameManager.instance.lastScore = _highScore;
        GameManager.instance.currentPoints = currentPoints;
        GameManager.instance.gamesPlayed++;
    }
}
