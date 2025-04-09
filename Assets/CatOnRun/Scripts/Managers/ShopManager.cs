using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

//キャラ数、金額、名前などは[managerVars]にて管理

public class ShopManager : MonoBehaviour
{

    //public GameObject shopItemName;

    public GameObject scrollContent, shopItemPrefab, shopMenu, shopPlay, shopBuy;
    public Button closeShop, shopSelectButton, openShop;
    public Text shopSelectButtonText;
    public ScrollRect shopScroll;
    public string shopState;
    public ScrollRect scroll;
    public int scrollItemWidth, characterIndex, unlockableItemIndex;
    public managerVars vars;

    void OnEnable()
    {
        vars = Resources.Load<managerVars>("managerVarsContainer");
    }

    // Use this for initialization
    void Start()
    {
        closeShop.GetComponent<Button>().onClick.AddListener(() => { CloseShopMenu(); });
        openShop.GetComponent<Button>().onClick.AddListener(() => { OpenShopMenu(); });
        shopSelectButton.GetComponent<Button>().onClick.AddListener(() => { SelectCharacter(); });
    }

    // Update is called once per frame
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
            characterIndex = Mathf.Abs(Mathf.FloorToInt(curLoc));
        }
        else if (type62 <= -(scrollItemWidth / 2))
        {
            characterIndex = Mathf.Abs(Mathf.CeilToInt(curLoc));
        }
        //check if shop menu is active
        if (shopMenu.activeSelf)
        {
            //if yes then we set the position of character images
            //キャラクターの表示設定
            for (int i = 0; i <= scrollContent.transform.childCount - 1; i++)
            {
                //we make the selected image size to its full size
                //選択したキャラクターの表示サイズ
                if (i == characterIndex)
                {
                    scrollContent.transform.GetChild(characterIndex).GetChild(0).GetComponent<RectTransform>().sizeDelta = new Vector2(300, 240);
                }
                //and we make the un-selected image size to its half size
                //選択してないキャラクターの表示サイズ
                else
                {
                    scrollContent.transform.GetChild(i).GetChild(0).GetComponent<RectTransform>().sizeDelta = new Vector2(200, 150f);
                }
            }

            //we then sets the name of character
            //キャラクター名の設定
            //shopItemName.GetComponent<Text>().text = vars.characters[characterIndex].characterName;

            //set the button
            //アンロックおよびロックのボタン表示設定
            if (GameManager.instance.skinUnlocked[characterIndex])
            {
                shopPlay.SetActive(true);
                shopBuy.SetActive(false);
            }
            else
            {
                shopPlay.SetActive(false);
                shopBuy.SetActive(true);
                shopSelectButtonText.text = "" + vars.characters[characterIndex].characterPrice;
            }
        }

    }// Update

    //method called to open the shop
    public void OpenShopMenu()
    {
        GuiManager.instance.ButtonPress();
        GuiManager.instance.mainMenuPanel.SetActive(false);
        shopMenu.SetActive(true);
        UpdateShopItems();
    }

    //method called to close the shop
    public void CloseShopMenu()
    {
        GuiManager.instance.ButtonPress();
        foreach (Transform child in scrollContent.transform)
        {
            Destroy(child.gameObject);
        }

        shopMenu.SetActive(false);
        GuiManager.instance.mainMenuPanel.SetActive(true);
    }

    //method called by select button to select the character
    public void SelectCharacter()
    {
        GuiManager.instance.ButtonPress();
        if (GameManager.instance.skinUnlocked[characterIndex])
        {
            GameManager.instance.selectedSkin = characterIndex;
            GameManager.instance.Save();
            string sceneName = SceneManager.GetActiveScene().name;
            SceneManager.LoadScene(sceneName);
        }
        else if (GameManager.instance.points >= vars.characters[characterIndex].characterPrice)
        {
            GameManager.instance.points -= vars.characters[characterIndex].characterPrice;
            GameManager.instance.skinUnlocked[characterIndex] = true;
            GameManager.instance.selectedSkin = characterIndex;
            GameManager.instance.Save();

            //we 1st destroy all the gameobjects
            foreach (Transform child in scrollContent.transform)
            {
                Destroy(child.gameObject);
            }
            //then update the shop
            UpdateShopItems();

        }
        else
        {
            Debug.Log("Buy Coins");
        }
    }

    //method which controls the movement and scrolling and spawning image prefabs 
    public void UpdateShopItems()
    {
        //set the scrollContent parent size
        //スクロール背景の設定1
        scrollContent.transform.parent.GetComponent<RectTransform>().sizeDelta = new Vector2((vars.characters.Count * scrollItemWidth) + 850f, 380f);
        //set the scrollContent size
        //スクロール背景の設定2
        scrollContent.GetComponent<RectTransform>().sizeDelta = new Vector2((vars.characters.Count * scrollItemWidth) + 850f, 200f);
        //loop through all the characters
        for (int i = 0; i <= (vars.characters.Count - 1); i++)
        {   //spawn the prefab
            GameObject shopItem = Instantiate(shopItemPrefab, new Vector3(0, 0, 0), Quaternion.Euler(0, 0, 0)) as GameObject;
            shopItem.transform.SetParent(scrollContent.transform);//set its parent
            shopItem.transform.localRotation = Quaternion.Euler(0, 0, 0);//set its rotation
            shopItem.transform.localScale = new Vector3(1, 1, 1);//set its scale
            if (i == 0) //the prefas at 0 index
            {
                //選択されたキャラクターの位置
                shopItem.transform.localPosition = new Vector3(418f, 50.0f, 0);
            }
            else
            {
                //set other next prefabs position
                //選択されてないキャラクター位置
                shopItem.transform.localPosition = new Vector3((scrollItemWidth * i) + 418f, 50.0f, 0);
            }
            //we get the tranform of the object which has Image component on it(image prefab)
            Transform x = shopItem.transform.GetChild(0).GetChild(0);
            x.GetComponent<Image>().sprite = vars.characters[i].characterSprite;
        }
        //when we open the shop the scroll is set to show the selected object in middle
        shopScroll.content.anchoredPosition = new Vector2(-(GameManager.instance.selectedSkin * scrollItemWidth), 0f);
    }

}//class
