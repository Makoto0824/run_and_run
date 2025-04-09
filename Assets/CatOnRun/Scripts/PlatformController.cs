using UnityEngine;

public class PlatformController : MonoBehaviour {

    public bool deactivateAtLimit = true;//to restrict the start platfrom from deactivating

    [SerializeField]
    private SpriteRenderer[] top, bottom;//ref to the sprites which make the platform

    private GameObject cameraObj; //ref to camera
    [SerializeField]
    private float distFromCamera = -15.25f;//deactivate platfrom after limit distance is crossed
    private bool spawn = true;
    private int i = 0;

    public managerVars vars;

    void OnEnable()
    {
        vars = Resources.Load<managerVars>("managerVarsContainer");
    }

    // Use this for initialization
    void Start ()
    {   //get ref to camera
        cameraObj = GameObject.FindGameObjectWithTag("MainCamera");
        SetTileImages();//set the tile sprites
    }

    // Update is called once per frame
    private void Update()
    {   //if tileset is changes and i is 0
        if (GameManager.instance.tileSetChanged && i == 0)
        {
            i = 1; //set i to 1
            SetTileImages();//set the images
        }

        //if the distance between platfrom and camera is less than 0 and spawn is true
        if (transform.position.x - cameraObj.transform.position.x <= 0 && spawn)
        {
            spawn = false;//set spawn to false
            PlatformSpawner.instance.SpawnPlatform();//spawn the new platform
        }
        //if the distance between platfrom and camera is less than requried distance
        if (transform.position.x - cameraObj.transform.position.x <= distFromCamera && deactivateAtLimit)
        {   //deactiavte the platfrom
            gameObject.SetActive(false);
        }
    }
    //called when platfrom is spawned
    public void BasicSettings()
    {
        spawn = true;
    }
    //method which set the sprites
    void SetTileImages()
    {
        //top tile
        if (top.Length != 0)
        {   //loop through all the images
            for (int i = 0; i < top.Length; i++)
            {   //and set it to the selected world
                top[i].sprite = vars.themeData[GameManager.instance.selectedTheme].topTile;
            }
        }

        //bottom top tile
        if (bottom.Length != 0)
        {
            for (int i = 0; i < bottom.Length; i++)
            {
                bottom[i].sprite = vars.themeData[GameManager.instance.selectedTheme].bottomTile;
            }
        }
    }
}