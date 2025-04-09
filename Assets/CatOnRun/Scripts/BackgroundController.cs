using UnityEngine;

public class BackgroundController : MonoBehaviour {

    public float bgSpeed; //speed of background 
    public Renderer mainBackground;//ref to renderer
    public managerVars vars;//ref to managerVars
    private int i = 0;//to set background texture only once in each game

    void OnEnable()
    {
        vars = Resources.Load<managerVars>("managerVarsContainer");
    }

    // Use this for initialization
    void Start()
    {
        SetBackground();
    }

    // Update is called once per frame
    void Update()
    {   //checks if the tile set is changed and i == 0
        if (GameManager.instance.tileSetChanged && i == 0)
        {
            i = 1;//set i to 1
            SetBackground();//set the background
        }

        ScrollBG(bgSpeed, mainBackground);//apply scrolling effect
    }
    //method which make background scroll , take float and renderer variables
    void ScrollBG(float scrollSpeed, Renderer rend)
    {   //calculates the offset
        float offset = Camera.main.transform.position.x * scrollSpeed;
        //then set the offset of texture on material
        rend.material.SetTextureOffset("_MainTex", new Vector2(offset, -0.001f));
    }

    void SetBackground()
    {
        //check if the selected them index is 0
        if (GameManager.instance.selectedTheme == 0)
        {   //set its tiling to 6,6
            mainBackground.material.mainTextureScale = new Vector2(6, 6);
        }//check if the selected them index is not 0
        else if (GameManager.instance.selectedTheme != 0)
        {   //set its tiling to 1,1
            mainBackground.material.mainTextureScale = new Vector2(1, 1);
        }
        //set the texture from the managers saved element
        mainBackground.material.mainTexture = vars.themeData[GameManager.instance.selectedTheme].backgroundTexture;
    }

}
