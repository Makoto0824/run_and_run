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
    public ScrollRect worldScroll;
    public string worldState;
    public ScrollRect scroll;
    public int scrollItemWidth, worldIndex, unlockableItemIndex;
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
        //current Location
        float curLoc = scroll.content.anchoredPosition.x / scrollItemWidth;
        //location to rach
        float locToReach = Mathf.Floor(curLoc);
        float posBetween = locToReach - curLoc;
        float type62 = posBetween * scrollItemWidth;

        // Update Pos
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

        // Update Index
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
				//選択したマップの画像のサイズ設定
                if (i == worldIndex)
                {
                    scrollContent.transform.GetChild(worldIndex).GetChild(0).GetComponent<RectTransform>().sizeDelta = new Vector2(250, 250);
                }
				//選択したマップ以外の画像のサイズ設定
                else
                {
                    scrollContent.transform.GetChild(i).GetChild(0).GetComponent<RectTransform>().sizeDelta = new Vector2(200, 100f);
                }
            }
          
            //ステージ名の設定
            //worldItemName.GetComponent<Text>().text = vars.themes[worldIndex].themeName;
            
			//表示ボタンの設定（購入済みか否か）
            if (GameManager.instance.themeUnlocked[worldIndex] == true)
            {
                worldPlay.SetActive(true);
                worldBuy.SetActive(false);
            }
            else if (GameManager.instance.themeUnlocked[worldIndex] == false)
            {
                worldPlay.SetActive(false);
                worldBuy.SetActive(true);
                worldSelectButtonText.text = "" + vars.themes[worldIndex].themePrice;
            }
        }

    }// Update

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
        if (GameManager.instance.themeUnlocked[worldIndex] == true)
        {
            GameManager.instance.selectedTheme = worldIndex;
            GameManager.instance.Save();
            GuiManager.instance.PlayBtn();
            GameManager.instance.tileSetChanged = true;
        }
        else if (GameManager.instance.points >= vars.themes[worldIndex].themePrice)
        {
            GameManager.instance.points -= vars.themes[worldIndex].themePrice;
            GameManager.instance.themeUnlocked[worldIndex] = true;
            GameManager.instance.selectedTheme = worldIndex;
            GameManager.instance.Save();

            //we 1st destroy all the gameobjects
            foreach (Transform child in scrollContent.transform)
            {
                Destroy(child.gameObject);
            }
            //then update the shop
            UpdateShopItems();

        }
        else if (GameManager.instance.points < vars.themes[worldIndex].themePrice)
        {
            Debug.Log("Buy Coins");
        }
    }

    //method which controls the movement and scrolling and spawning image prefabs 
    public void UpdateShopItems()
    {   //set the scrollContent parent size
        scrollContent.transform.parent.GetComponent<RectTransform>().sizeDelta = new Vector2((vars.themes.Count * scrollItemWidth) + 850f, 380f);
        //set the scrollContent size
        scrollContent.GetComponent<RectTransform>().sizeDelta = new Vector2((vars.themes.Count * scrollItemWidth) + 850f, 280f);
        //loop through all the themes
        for (int i = 0; i <= (vars.themes.Count - 1); i++)
        {   //spawn the prefab
            GameObject shopItem = Instantiate(worldItemPrefab, new Vector3(0, 0, 0), Quaternion.Euler(0, 0, 0)) as GameObject;
            shopItem.transform.SetParent(scrollContent.transform);//set its parent
            shopItem.transform.localRotation = Quaternion.Euler(0, 0, 0);//set its rotation
            shopItem.transform.localScale = new Vector3(1, 1, 1);//set its scale
            if (i == 0) //the prefas at 0 index
            {   //set its position
                shopItem.transform.localPosition = new Vector3(418f, 50.0f, 0);
            }
            else
            {   //set other next prefabs position
				shopItem.transform.localPosition = new Vector3((scrollItemWidth * i) + 418f, 50.0f, 0);
            }
            //we get the tranform of the object which has Image component on it(image prefab)
            Transform x = shopItem.transform.GetChild(0).GetChild(0);
            if (i == 0)
            {   //set the image value
                x.GetComponent<Image>().sprite = vars.themes[0].shopThemeSprite;
            }
            else
            {   //set the image value
                x.GetComponent<Image>().sprite = vars.themes[i].shopThemeSprite;
                //if (GameManager.instance.themeUnlocked[i] == false)
                //{   //if the themes are unlocked then there color is set
                //    x.GetComponent<Image>().color = new Color32(0, 0, 0, 150);
                //    //x.GetComponent<Image>().color = new Color32(0, 0, 0, 150);
                //}
            }
        }
        //when we open the shop the scroll is set to show the selected object in middle
        worldScroll.content.anchoredPosition = new Vector2(-(GameManager.instance.selectedTheme * scrollItemWidth), 0f);
    }

}//class