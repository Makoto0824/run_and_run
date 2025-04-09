using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;


//ステージ数、金額、名前などは[managerVars]にて管理

public class WorldShopManager : MonoBehaviour
{
    public static WorldShopManager instance;

    //ステージ名
    //public GameObject worldItemName; 

    public GameObject scrollContent, worldItemPrefab, worldMenu, worldPlay, worldBuy;
    public Button closeWorld, worldSelectButton, openWorld;
    public Text worldSelectButtonText;
    public ScrollRect scroll;
    public int scrollItemWidth, worldIndex;
    public managerVars vars;

    void OnEnable()
    {
        vars = Resources.Load<managerVars>("managerVarsContainer");
    }

    void Awake()
    {
        if (instance == null)
            instance = this;
    }

    void Start()
    {
        closeWorld.GetComponent<Button>().onClick.AddListener(() => { CloseWorldMenu(); });
        openWorld.GetComponent<Button>().onClick.AddListener(() => { OpenWorldMenu(); });
        worldSelectButton.GetComponent<Button>().onClick.AddListener(() => { SelectWorld(); });
    }

    void Update()
    {
        float curLoc = scroll.content.anchoredPosition.x / scrollItemWidth;
        float locToReach = Mathf.Floor(curLoc);
        float posBetween = locToReach - curLoc;
        float type62 = posBetween * scrollItemWidth;

        if (Input.GetMouseButtonUp(0))
        {
            if (type62 >= -(scrollItemWidth / 2) + 1)
            {
                scroll.content.anchoredPosition = new Vector2(-Mathf.Floor(curLoc) * -scrollItemWidth, 0f);
            }
            else if (type62 <= -(scrollItemWidth / 2))
            {
                scroll.content.anchoredPosition = new Vector2(-Mathf.Ceil(curLoc) * -scrollItemWidth, 0f);
            }
        }

        if (type62 >= -(scrollItemWidth / 2) + 1)
        {
            worldIndex = Mathf.Abs(Mathf.FloorToInt(curLoc));
        }
        else if (type62 <= -(scrollItemWidth / 2))
        {
            worldIndex = Mathf.Abs(Mathf.CeilToInt(curLoc));
        }

        if (worldMenu.activeSelf)
        {
            for (int i = 0; i <= scrollContent.transform.childCount - 1; i++)
            {
                if (i == worldIndex)
                {
                    scrollContent.transform.GetChild(worldIndex).GetChild(0).GetComponent<RectTransform>().sizeDelta = new Vector2(250, 250);
                }
                else
                {
                    scrollContent.transform.GetChild(i).GetChild(0).GetComponent<RectTransform>().sizeDelta = new Vector2(200, 100f);
                }
            }

            if (GameManager.instance.themeUnlocked[worldIndex])
            {
                worldPlay.SetActive(true);
                worldBuy.SetActive(false);
            }
            else
            {
                worldPlay.SetActive(false);
                worldBuy.SetActive(true);
                worldSelectButtonText.text = $"{vars.themes[worldIndex].themePrice}";
            }
        }
    }

    //method called to open the world
    public void OpenWorldMenu()
    {
        GuiManager.instance.ButtonPress();
        GuiManager.instance.mainMenuPanel.SetActive(false);
        worldMenu.SetActive(true);
        UpdateShopItems();
    }

    //method called to close the world
    public void CloseWorldMenu()
    {
        GuiManager.instance.ButtonPress();
        foreach (Transform child in scrollContent.transform)
        {
            Destroy(child.gameObject);
        }

        worldMenu.SetActive(false);
        GuiManager.instance.mainMenuPanel.SetActive(true);
    }

    //method called by select button to select the world
    public void SelectWorld()
    {
        GuiManager.instance.ButtonPress();
        if (GameManager.instance.themeUnlocked[worldIndex])
        {
            GameManager.instance.selectedTheme = worldIndex;
            GameManager.instance.Save();
            UpdateShopItems();

            // プレイヤーを生成
            if (PlayerSpawner.instance != null)
            {
                PlayerSpawner.instance.SpawnPlayer();
            }

            // ゲーム開始画面を表示
            worldMenu.SetActive(false);
            GuiManager.instance.inGamePanel.SetActive(true);
            GuiManager.instance.gameMenuPanel.SetActive(true);
            GuiManager.instance.tutorialPanel.SetActive(true);
            GuiManager.instance.gameStartBtn.SetActive(true);
        }
        else
        {
            if (GameManager.instance.points >= vars.themes[worldIndex].themePrice)
            {
                GameManager.instance.points -= vars.themes[worldIndex].themePrice;
                GameManager.instance.themeUnlocked[worldIndex] = true;
                GameManager.instance.Save();
                GuiManager.instance.shopStarText.text = $"{GameManager.instance.points}";
                GuiManager.instance.worldStarText.text = $"{GameManager.instance.points}";
                worldPlay.SetActive(true);
                worldBuy.SetActive(false);
            }
        }
    }

    //method which controls the movement and scrolling and spawning image prefabs 
    public void UpdateShopItems()
    {
        scrollContent.transform.parent.GetComponent<RectTransform>().sizeDelta = new Vector2((vars.themes.Count * scrollItemWidth) + 850f, 380f);
        scrollContent.GetComponent<RectTransform>().sizeDelta = new Vector2((vars.themes.Count * scrollItemWidth) + 850f, 280f);

        for (int i = 0; i <= (vars.themes.Count - 1); i++)
        {
            GameObject shopItem = Instantiate(worldItemPrefab, new Vector3(0, 0, 0), Quaternion.Euler(0, 0, 0)) as GameObject;
            shopItem.transform.SetParent(scrollContent.transform);
            shopItem.transform.localRotation = Quaternion.Euler(0, 0, 0);
            shopItem.transform.localScale = new Vector3(1, 1, 1);
            shopItem.transform.localPosition = new Vector3((scrollItemWidth * i) + 418f, 50.0f, 0);

            Transform x = shopItem.transform.GetChild(0).GetChild(0);
            if (i == 0)
            {
                x.GetComponent<Image>().sprite = vars.themes[0].shopThemeSprite;
            }
            else
            {
                x.GetComponent<Image>().sprite = vars.themes[i].shopThemeSprite;
            }
        }

        scroll.content.anchoredPosition = new Vector2(-(GameManager.instance.selectedTheme * scrollItemWidth), 0f);
    }

}//class