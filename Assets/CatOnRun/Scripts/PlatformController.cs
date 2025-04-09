using UnityEngine;

public class PlatformController : MonoBehaviour
{
    public bool deactivateAtLimit = true;

    [SerializeField]
    private SpriteRenderer[] top, bottom;

    private GameObject cameraObj;
    [SerializeField]
    private float distFromCamera = -15.25f;
    private bool spawn = true;
    private bool tileSetUpdated = false;

    public managerVars vars;

    void OnEnable()
    {
        vars = Resources.Load<managerVars>("managerVarsContainer");
    }

    void Start()
    {
        cameraObj = GameObject.FindGameObjectWithTag("MainCamera");
        SetTileImages();
    }

    private void Update()
    {
        if (GameManager.instance.tileSetChanged && !tileSetUpdated)
        {
            tileSetUpdated = true;
            SetTileImages();
        }

        if (transform.position.x - cameraObj.transform.position.x <= 0 && spawn)
        {
            spawn = false;
            PlatformSpawner.instance.SpawnPlatform();
        }

        if (transform.position.x - cameraObj.transform.position.x <= distFromCamera && deactivateAtLimit)
        {
            gameObject.SetActive(false);
        }
    }

    public void BasicSettings()
    {
        spawn = true;
    }

    void SetTileImages()
    {
        if (top.Length != 0)
        {
            foreach (var spriteRenderer in top)
            {
                spriteRenderer.sprite = vars.themeData[GameManager.instance.selectedTheme].topTile;
            }
        }

        if (bottom.Length != 0)
        {
            foreach (var spriteRenderer in bottom)
            {
                spriteRenderer.sprite = vars.themeData[GameManager.instance.selectedTheme].bottomTile;
            }
        }
    }
}